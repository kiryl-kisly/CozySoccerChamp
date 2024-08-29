import { IMatchResultResponse } from './IMatchResultResponse'
import { ITeamResponse } from './ITeamResponse'

export interface IMatchResponse {
	matchId: number | null
	startTimeUtc: Date | null
	group: string | null
	stage: string | null
	matchDay: number | null
	competitionId: number | null
	teamHome: ITeamResponse | null | null
	teamAway: ITeamResponse | null | null
	matchResult: IMatchResultResponse | null
}	