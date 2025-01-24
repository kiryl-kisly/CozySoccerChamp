import { useEffect, useState } from 'react'
import './SettingsPage.css'

export function SettingsPage() {

	const [isHideFinishedMatches, setIsHideFinishedMatches] = useState<boolean>(() => {
		const saved = localStorage.getItem('isHideFinishedMatches')
		return saved !== null ? JSON.parse(saved) : false
	})
	useEffect(() => {
		localStorage.setItem('isHideFinishedMatches', JSON.stringify(isHideFinishedMatches))
	}, [isHideFinishedMatches])

	const toggleIsHideFinishedMatchesSwitch = () => {
		setIsHideFinishedMatches(!isHideFinishedMatches)
	}

	return (
		<>
			<h1 className='title-page'>Settings</h1>

			{/* Скрыть/показать завершенные матчи */}
			<div
				className='flex justify-between items-center ml-5 mr-5 text-lg'
				onClick={toggleIsHideFinishedMatchesSwitch}>
				<div className='absolute left-5 p-2'>Hide Finished Matches</div>
				<div className='ml-auto p-2'>
					<div className={`toggle ${isHideFinishedMatches ? 'toggle-active-color' : 'toggle-disable-color'}`}>
						<div className={`toggle-circle ${isHideFinishedMatches ? 'translate-x-8' : 'translate-x-0'}`}>
						</div>
					</div>
				</div>
			</div>
		</>

	)
}