import { AxiosResponse } from 'axios'
import axiosClient from './AxiosInstance'
import { IPredictionRequest } from './interfaces/Requests/IPredictionRequest'
import { IPredictionResponse } from './interfaces/Responses/IPredictionResponse'

export const makePrediction = async (request: IPredictionRequest) => {
	const response = await axiosClient().post<AxiosResponse>('prediction/makePrediction', request)

	return response
}

export const getPredictionsByMatchId = async (matchId: number | null) => {
	const response = await axiosClient().get<IPredictionResponse[]>(`prediction/getPredictionsByMatchId/${matchId}`)

	return response.data
}

export const getPredictionsByUserId = async () => {
	const response = await axiosClient().get<IPredictionResponse[]>('prediction/getPredictions')

	return response.data
}