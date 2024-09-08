import { IScoreResponse } from './IScoreResponse'

export interface IMatchResultResponse {
	matchResultId: number
	duration: string 
	status: string
	fullTime: IScoreResponse | null
	halfTime: IScoreResponse | null
	regularTime: IScoreResponse | null
	extraTime: IScoreResponse | null
	penalties: IScoreResponse | null
}