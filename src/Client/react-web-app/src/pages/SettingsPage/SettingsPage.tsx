import { useEffect, useState } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import { ToggleNotification } from '../../services/UserProfileService'
import { AppDispatch, RootState } from '../../store/store'
import { setEnabledNotification } from '../../store/userProfileSlice'
import './SettingsPage.css'

export function SettingsPage() {

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

	const dispatch = useDispatch<AppDispatch>()
	const userProfile = useSelector((state: RootState) => state.userProfile)

	const [isEnabledNotification, setIsEnabledNotification] = useState(userProfile.isEnabledNotification)

	const toggleIsEnabledNotifications = async () => {
		const newIsEnabledNotification = !isEnabledNotification
		setIsEnabledNotification(newIsEnabledNotification)
		// Отправляем действие в Redux Store
		dispatch(setEnabledNotification(newIsEnabledNotification))

		await ToggleNotification(!isEnabledNotification)
	}

	return (
		<>
			<h1 className='title-page'>Settings</h1>

			{/* Скрыть/показать завершенные матчи */}
			<div
				className='flex justify-between items-center ml-5 mr-5 text-lg'
				onClick={toggleIsHideFinishedMatches}>
				<div className='absolute left-5 p-2'>Hide Finished Matches</div>
				<div className='ml-auto p-2'>
					<div className={`toggle ${isHideFinishedMatches ? 'toggle-active-color' : 'toggle-disable-color'}`}>
						<div className={`toggle-circle ${isHideFinishedMatches ? 'translate-x-8' : 'translate-x-0'}`}>
						</div>
					</div>
				</div>
			</div>

			{/* Включить/выключить отправку уведомлений */}
			<div
				className='flex justify-between items-center ml-5 mr-5 text-lg'
				onClick={toggleIsEnabledNotifications}>
				<div className='absolute left-5 p-2'>Send Notifications</div>
				<div className='ml-auto p-2'>
					<div className={`toggle ${isEnabledNotification ? 'toggle-active-color' : 'toggle-disable-color'}`}>
						<div className={`toggle-circle ${isEnabledNotification ? 'translate-x-8' : 'translate-x-0'}`}>
						</div>
					</div>
				</div>
			</div>
		</>
	)
}
