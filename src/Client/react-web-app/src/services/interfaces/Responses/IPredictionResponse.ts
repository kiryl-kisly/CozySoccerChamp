import { IUserProfileResponse } from './IUserProfileResponse'

export interface IPredictionResponse {
	matchId: number
	predictedHomeScore: number | null
	predictedAwayScore: number | null
	pointPerMatch: number | null
	coefficient: number | null
	user: IUserProfileResponse | null
}
