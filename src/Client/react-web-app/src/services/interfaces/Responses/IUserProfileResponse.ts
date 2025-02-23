import { INotificationSettingsResponse } from './INotificationSettingsResponse'

export interface IUserProfileResponse {
	telegramUserId: number | undefined
	userName: string | undefined
	points: number
	place: number
	notificationSettings: INotificationSettingsResponse
}