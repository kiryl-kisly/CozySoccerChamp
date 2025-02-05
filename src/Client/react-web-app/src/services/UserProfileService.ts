import axiosClient from './AxiosInstance'
import { IUserProfileResponse } from './interfaces/Responses/IUserProfileResponse'

export const changeUserName = async (newUserName: string) => {

	const response = await axiosClient().post<IUserProfileResponse>('userProfile/changeUsername', {},
		{
			params: {
				'newUserName': newUserName
			}
		})

	return response.data
}

export const ToggleNotification = async (isEnabled: boolean | undefined) => {
	const response = await axiosClient().post<IUserProfileResponse>(`userProfile/ToggleNotification?isEnabled=${isEnabled}`)

	return response.data
}