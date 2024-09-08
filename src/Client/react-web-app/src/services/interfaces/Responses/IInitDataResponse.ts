import { IMatchResponse } from './IMatchResponse'
import { IPredictionResponse } from './IPredictionResponse'
import { IUserProfileResponse } from './IUserProfileResponse'

export interface IInitDataResponse {
	isLoading: boolean
	userProfile: IUserProfileResponse | null
	matches: IMatchResponse[] | null
	predictions: IPredictionResponse[] | null
}