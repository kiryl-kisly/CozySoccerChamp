import { IMatchResponse } from './IMatchResponse'
import { IUserProfileResponse } from './IUserProfileResponse'

export interface IInitDataResponse {
	isLoading: boolean
	userProfile: IUserProfileResponse | null
	matches: IMatchResponse[] | null
}