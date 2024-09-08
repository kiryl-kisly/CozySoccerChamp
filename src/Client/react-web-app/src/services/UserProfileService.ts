import axiosClient from './AxiosInstance'
import { IUserProfileResponse } from './interfaces/Responses/IUserProfileResponse'

export const changeUserName = async (userId: number, newUserName: string) => {

	const response = await axiosClient().post<IUserProfileResponse>('userProfile/changeUsername', {},
		{
			params: {
				'userId': userId,
				'newUserName': newUserName
			}
		})

	return response.data
}