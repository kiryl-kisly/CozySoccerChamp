import { IPredictionScoreRequest } from './IPredictionScoreRequest'

export interface IPredictionRequest {
	matchId: number
	prediction: IPredictionScoreRequest
}