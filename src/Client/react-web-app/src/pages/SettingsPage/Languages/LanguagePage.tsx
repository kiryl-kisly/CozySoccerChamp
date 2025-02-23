import { useBackButton } from '@telegram-apps/sdk-react'
import { useEffect } from 'react'
import { useNavigate } from 'react-router'
import { CheckBoxItem } from '../../../components/Setting/CheckBoxItem'

export function LanguagePage() {
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
			<h1 className='title-page'>Languages</h1>

			<p className="text-gray-400 font-thin mt-10">INTERFACE LANGUAGE</p>
			<div className="bg-[var(--gray-dark)] rounded-[10px] shadow w-full max-w-xl mx-auto">
				<CheckBoxItem title={'English'} isEnabled isLast />
			</div>

			<div className="mb-[75px]"></div>
		</>
	)
}