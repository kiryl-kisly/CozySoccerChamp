import axiosClient from './AxiosInstance'
import { IInitDataResponse } from './interfaces/Responses/IInitDataResponse'

export const getInitData = async (userId: number) => {
	const response = await axiosClient().get<IInitDataResponse>('initData/get',
		{
			params: {
				'userId': userId
			}
		})

	if (response.status === 200) {
		if (response.data.matches !== null)
			response.data.isLoading = false
	}

	return response.data
}