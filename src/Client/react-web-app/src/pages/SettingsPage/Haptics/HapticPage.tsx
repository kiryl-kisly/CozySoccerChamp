import { useBackButton } from '@telegram-apps/sdk-react'
import { useEffect } from 'react'
import { useNavigate } from 'react-router'

export function HapticPage() {
	const navigate = useNavigate()
	const backButton = useBackButton()

	useEffect(() => {
		backButton.show()
		const handleBackClick = () => navigate(-1)
		backButton.on("click", handleBackClick)

		return () => {
			backButton.hide()
			backButton.off("click", handleBackClick)
		}
	}, [backButton, navigate])

	return (
		<>
			<h1 className='title-page'>Haptic Settings</h1>

			<div>
				Coming soon...
			</div>

			<div className="mb-[75px]"></div>
		</>
	)
}