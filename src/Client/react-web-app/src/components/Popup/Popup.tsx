import { useEffect, useState } from 'react'
import { FaExclamationCircle } from 'react-icons/fa'
import { IoIosCheckmarkCircle } from 'react-icons/io'
import './Popup.css'

interface PopupProps {
	message: string
	isError: boolean
	duration: number
}

export function Popup({ message, isError = false, duration = 2000 }: PopupProps) {
	const [visible, setVisible] = useState(false)

	useEffect(() => {
		setVisible(true)

		const timer = setTimeout(() => {
			setVisible(false)
		}, duration)

		return () => clearTimeout(timer)
	}, [duration])

	const handleClose = () => {
		setVisible(false)
	}

	const text = isError
		? `Oops, something  wrong... ${message}`
		: message

	const icon = isError
		? <FaExclamationCircle color='#ff6600' size={20} />
		: <IoIosCheckmarkCircle color='#55ff00' size={20} />

	return (
		<div className={`popup ${visible ? 'popup-show' : 'popup-hide'} ${isError === false ? 'success' : 'error'}`}>
			<div className='popup-content'>
				<span className='popup-icon'>{icon}</span>
				<span>{text}</span>
			</div>
			<button className="popup-close" onClick={handleClose}>✖</button>
		</div>
	)
}