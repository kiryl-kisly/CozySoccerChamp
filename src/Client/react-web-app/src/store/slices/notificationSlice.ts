import { createSlice, PayloadAction } from '@reduxjs/toolkit'
import { INotificationSettingsResponse } from '../../services/interfaces/Responses/INotificationSettingsResponse'

export interface NotificationState {
	settings: INotificationSettingsResponse
}

const initialState: NotificationState = {
	settings: {
		isAnnouncement: false,
		isReminder: false,
		isForceReminder: false,
		reminderInterval: 60,
	},
}

const notificationSlice = createSlice({
	name: 'notification',
	initialState,
	reducers: {
		setNotificationSettings(state, action: PayloadAction<INotificationSettingsResponse>) {
			state.settings = action.payload
		},
		updateNotificationSetting(state, action: PayloadAction<Partial<INotificationSettingsResponse>>) {
			state.settings = { ...state.settings, ...action.payload }
		},
	},
})

export const { setNotificationSettings, updateNotificationSetting } = notificationSlice.actions
export default notificationSlice.reducer
