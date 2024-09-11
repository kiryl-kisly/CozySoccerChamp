import axiosClient from './AxiosInstance'
import { IMatchResponse } from './interfaces/Responses/IMatchResponse'

export const getStartedOrFinished = async () => {
	const response = await axiosClient().get<IMatchResponse[]>('match/getStartedOrFinished', {})

	return response.data
}