import { IMatchResponse } from '../../services/interfaces/IMatchResponse'
import './MatchCard.css'

export function MatchCard({ match }: IMatchResponse) {
	return (
		<>
			<div className='prediction-card'>
				<div className='team'>
					<div className='team-icon'>
						<img src={match.teamHome.emblemUrl}></img>
					</div>
					<div className='team-name'>{match.teamHome.shortName}</div>
				</div>
				<div className='score'>
					<div className='actual-score'>{match.matchResult.fullTime.homeTeamScore} | {match.matchResult.fullTime.awayTeamScore}</div>
					<div className='prediction-text'>Your prediction</div>
					<div className='predicted-score'>2 | 3</div>
				</div>
				<div className='team'>
					<div className='team-icon'>
						<img src={match.teamAway.emblemUrl}></img>
					</div>
					<div className='team-name'>{match.teamAway.shortName}</div>
				</div>
				{/* <div className='points'>
					<div className='points-value'>+5</div>
					<div className='points-text'>Points</div>
				</div> */}
			</div>
		</>
	)
}