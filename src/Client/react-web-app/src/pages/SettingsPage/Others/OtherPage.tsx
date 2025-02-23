import { useBackButton } from '@telegram-apps/sdk-react'
import { useEffect, useState } from 'react'
import { Link, useNavigate } from 'react-router-dom'
import { ToggleItem } from '../../../components/Setting/ToggleItem'

export function OtherPage() {
	const [isHideFinishedMatches, setIsHideFinishedMatches] = useState<boolean>(() => {
		const saved = localStorage.getItem('isHideFinishedMatches')
		return saved !== null ? JSON.parse(saved) : false
	})

	useEffect(() => {
		localStorage.setItem('isHideFinishedMatches', JSON.stringify(isHideFinishedMatches))
	}, [isHideFinishedMatches])

	const toggleIsHideFinishedMatches = () => {
		setIsHideFinishedMatches(!isHideFinishedMatches)
	}

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
			<h1 className='title-page'>Other Settings</h1>

			<div className="bg-[var(--gray-dark)] rounded-[10px] shadow w-full max-w-xl mx-auto">
				<ToggleItem title={'Hide Finished Matches'} isActive={isHideFinishedMatches} onClick={toggleIsHideFinishedMatches} />
			</div>
			<p className='text-gray-400 ml-4 font-thin mt-2'>
				Hide information for finished matches in{" "}
				<Link to="/matches" className="text-blue-400 hover:underline">
					Matches Page
				</Link>
			</p>
		</>
	)
}
