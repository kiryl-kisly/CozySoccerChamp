import { ICompetitionResponse } from './ICompetitionResponse'
import { ILeaderboardResponse } from './ILeaderboardResponse'
import { IMatchResponse } from './IMatchResponse'
import { IPredictionResponse } from './IPredictionResponse'
import { IUserProfileResponse } from './IUserProfileResponse'

export interface IInitDataResponse {
	isLoading: boolean | true
	userProfile: IUserProfileResponse | null
	competition: ICompetitionResponse | null
	matches: IMatchResponse[] | null
	predictions: IPredictionResponse[] | null
	leaderboard: ILeaderboardResponse[] | null
}