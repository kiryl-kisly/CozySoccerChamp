import { MatchCard } from '../../components/Match/MatchCard'
import { IMatchResponse } from '../../services/interfaces/IMatchResponse'

export function MatchesPage({ matches }: IMatchResponse) {
	return (
		<>

			{matches.map((match: IMatchResponse, index: number) => (
				<MatchCard match={match} />
				// <div key={index}>
				// 	<span>{match.teamHome?.shortName} - {match.teamAway?.shortName}</span>
				// </div>
			))}
		</>
	)
}