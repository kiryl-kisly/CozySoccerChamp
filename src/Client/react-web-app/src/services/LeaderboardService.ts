import axiosClient from './AxiosInstance'
import { ILeaderboardResponse } from './interfaces/Responses/ILeaderboardResponse'

export const getLeaderboard = async () => {
	const response = await axiosClient().get<ILeaderboardResponse[]>('leaderboard/get', {})

	console.log(response.data)
	return response.data
}
