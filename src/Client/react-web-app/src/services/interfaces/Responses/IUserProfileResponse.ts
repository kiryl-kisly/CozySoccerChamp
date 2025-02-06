export interface IUserProfileResponse {
	telegramUserId: number | undefined
	userName: string | undefined
	isEnabledNotification: boolean
	points: number
	place: number
}