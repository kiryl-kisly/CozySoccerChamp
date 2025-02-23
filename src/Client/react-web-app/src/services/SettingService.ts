import axiosClient from './AxiosInstance'
import { INotificationSettingsResponse } from './interfaces/Responses/INotificationSettingsResponse'
import { IUserProfileResponse } from './interfaces/Responses/IUserProfileResponse'

export const updateNotification = async (body: INotificationSettingsResponse) => {
	const response = await axiosClient().put<IUserProfileResponse>('setting/notifications/update', body)
	return response
}