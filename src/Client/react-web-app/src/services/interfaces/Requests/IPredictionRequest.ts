import { IPredictionScoreRequest } from './IPredictionScoreRequest'

export interface IPredictionRequest {
	userId: number
	matchId: number
	prediction: IPredictionScoreRequest
}