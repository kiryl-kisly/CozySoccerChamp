import { useBackButton } from '@telegram-apps/sdk-react'
import { AxiosError } from 'axios'
import { useEffect, useState } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import { useNavigate } from 'react-router'
import { Popup } from '../../../components/Popup/Popup'
import { IntervalItem } from '../../../components/Setting/IntervalItem'
import { ToggleItem } from '../../../components/Setting/ToggleItem'
import { INotificationSettingsResponse } from '../../../services/interfaces/Responses/INotificationSettingsResponse'
import { updateNotification } from '../../../services/SettingService'
import { setNotificationSettings } from '../../../store/slices/notificationSlice'
import { RootState } from '../../../store/store'

export function NotificationPage() {
	const dispatch = useDispatch()
	const notificationSettings = useSelector((state: RootState) => state.notification.settings)

	const [initialSettings, setInitialSettings] = useState(notificationSettings)

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

	useEffect(() => {
		setInitialSettings(notificationSettings)
	}, [])

	const isChanged =
		notificationSettings.isAnnouncement !== initialSettings.isAnnouncement ||
		notificationSettings.isReminder !== initialSettings.isReminder ||
		notificationSettings.isForceReminder !== initialSettings.isForceReminder ||
		notificationSettings.reminderInterval !== initialSettings.reminderInterval

	const [popupMessage, setPopupMessage] = useState<string | null>(null)
	const [isError, setIsError] = useState<boolean>(false)

	const handleSaveSettings = async () => {
		if (!isChanged)
			return

		try {
			setPopupMessage(null)

			const request: INotificationSettingsResponse = {
				isAnnouncement: notificationSettings.isAnnouncement,
				isReminder: notificationSettings.isReminder,
				isForceReminder: notificationSettings.isForceReminder,
				reminderInterval: notificationSettings.reminderInterval
			}

			const result = await updateNotification(request)
			if (result.status === 200 && result.statusText === 'OK') {
				dispatch(setNotificationSettings(request))
				setInitialSettings(request)

				setPopupMessage('Your settings have been successfully saved')
				setIsError(false)
			}
		} catch (error) {
			const axiosError = error as AxiosError<{ message?: string }>
			const errorMessage = axiosError.response?.data?.message || axiosError.message

			setPopupMessage(errorMessage)
			setIsError(true)
		}
	}

	return (
		<>
			<h1 className="title-page">Notifications</h1>

			<div className="bg-[var(--gray-dark)] rounded-[10px] shadow w-full max-w-xl mx-auto">
				<ToggleItem
					title="Announcement"
					isActive={notificationSettings.isAnnouncement}
					onClick={() => dispatch(setNotificationSettings({ ...notificationSettings, isAnnouncement: !notificationSettings.isAnnouncement }))}
				/>
			</div>
			<p className="text-gray-400 ml-4 font-thin text-sm mt-2">
				Notifications of announcements of new matchday starts.
			</p>

			<p className="text-gray-400 font-thin mt-10">REMINDERS</p>
			<div className="bg-[var(--gray-dark)] rounded-[10px] shadow w-full max-w-xl mx-auto">
				<ToggleItem
					title="Reminder"
					isActive={notificationSettings.isReminder}
					onClick={() => dispatch(setNotificationSettings({ ...notificationSettings, isReminder: !notificationSettings.isReminder }))}
				/>

				<ToggleItem
					title="Force Reminder"
					isActive={notificationSettings.isForceReminder}
					onClick={() => dispatch(setNotificationSettings({ ...notificationSettings, isForceReminder: !notificationSettings.isForceReminder }))}
					disabled={!notificationSettings.isReminder}
				/>

				<IntervalItem
					options={[60, 30, 15]}
					selected={notificationSettings.reminderInterval}
					onSelect={(interval) => dispatch(setNotificationSettings({ ...notificationSettings, reminderInterval: interval }))}
					disabled={!notificationSettings.isReminder}
				/>
			</div>

			<p className="text-gray-400 ml-4 font-thin text-sm mt-2">
				Notification if you forgot to make a prediction on matches with the interval for what time it is required to send a notification.
			</p>

			{/* Кнопка сохранения */}
			<button
				onClick={handleSaveSettings}
				className={`mt-10 w-full max-w-xs mx-auto block py-2 px-4 text-white rounded-lg transition ${isChanged ? 'bg-[var(--green)] hover:bg-green-600' : 'bg-gray-500 cursor-not-allowed'
					}`}
				disabled={!isChanged}
			>
				Save Settings
			</button>

			{popupMessage && <Popup message={popupMessage} isError={isError} duration={2000} />}
		</>
	)
}
