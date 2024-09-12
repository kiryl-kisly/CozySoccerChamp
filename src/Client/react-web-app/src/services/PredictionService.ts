import { AxiosResponse } from 'axios'
import axiosClient from './AxiosInstance'
import { IPredictionRequest } from './interfaces/Requests/IPredictionRequest'
import { IPredictionResponse } from './interfaces/Responses/IPredictionResponse'

export const makePrediction = async (request: IPredictionRequest) => {
	console.log(request)
	const response = await axiosClient().post<AxiosResponse>('prediction/makePrediction', request)

	return response
}

export const getPredictionsByMatchId = async (matchId: number | null) => {
	const response = await axiosClient().get<IPredictionResponse[]>(`Prediction/GetPredictionsByMatchId/${matchId}`)

	return response.data
}