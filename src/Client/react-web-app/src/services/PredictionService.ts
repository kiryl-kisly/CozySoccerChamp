import { AxiosResponse } from 'axios'
import axiosClient from './AxiosInstance'
import { IPredictionRequest } from './interfaces/Requests/IPredictionRequest'

export const makePrediction = async (request: IPredictionRequest) => {
	console.log(request)
	const response = await axiosClient().post<AxiosResponse>('prediction/makePrediction', request)

	return response
}