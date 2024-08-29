import { useState } from 'react'
import { Popup } from '../../components/Popup/Popup'

export function SettingsPage() {
	const [popupMessage, setPopupMessage] = useState<string | null>(null)

	const handleUserAction = () => {
		setPopupMessage(null)

		setTimeout(() => {
			setPopupMessage('Your action was saved successfully')
		}, 0)
	}

	return (
		<>
			<h1 className='text-white'>SettingsPage</h1>
			<div className='text-white p-20'>
				<button onClick={handleUserAction}>Save</button>
				{popupMessage && <Popup message={popupMessage} />}
			</div>
		</>

	)
}