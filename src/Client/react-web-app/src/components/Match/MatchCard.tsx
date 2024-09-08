import { useState } from 'react'
import { IPredictionRequest } from '../../services/interfaces/Requests/IPredictionRequest'
import { makePrediction } from '../../services/PredictionService'
import { Popup } from '../Popup/Popup'
import './MatchCard.css'

export function MatchCard({ match, prediction }) {
	const [homePredictedCount, setHomePredictedCount] = useState(prediction?.predictedHomeScore ?? 0)
	const [awayPredictedCount, setAwayPredictedCount] = useState(prediction?.predictedAwayScore ?? 0)

	const minPredictionValue = 0
	const maxPredictionValue = 20

	const updatePredictionValue = (_value: number, setValue: React.Dispatch<React.SetStateAction<number>>, change: number) => {
		setValue(prevValue => Math.min(maxPredictionValue, Math.max(minPredictionValue, prevValue + change)))
	}

	const [popupMessage, setPopupMessage] = useState<string | null>(null)
	const [isError, setIsError] = useState<boolean>(false)

	const sendRequest = async () => {
		try {
			setPopupMessage(null)

			const request: IPredictionRequest =
			{
				userId: 1,
				matchId: match.matchId,
				prediction: { predictedHomeScore: homePredictedCount, predictedAwayScore: awayPredictedCount }
			}

			const result = await makePrediction(request)
			if (result.status === 200 && result.statusText === 'OK') {
				setPopupMessage('Your action was saved successfully')
				setIsError(false)
			}
		} catch (error) {
			const errorMessage = error.response?.data?.message || error.message
			setPopupMessage(errorMessage)
			setIsError(true)
		}
	}


	return (
		<>
			<div className='wrapper'>
				<div className='team-wrapper'>
					{match.teamHome ? (
						<>
							<div className='team-icon'>
								<img src={match.teamHome.emblemUrl} alt='Team Emblem' />
							</div>
							<div className='team-name'>{match.teamHome.shortName}</div>
							<div className='prediction-wrapper text-4xl'>
								<div>
									<button
										onClick={() =>
											updatePredictionValue(homePredictedCount, setHomePredictedCount, -1)}>
										-
									</button>
								</div>
								<div>{homePredictedCount}</div>
								<div>
									<button
										onClick={() =>
											updatePredictionValue(homePredictedCount, setHomePredictedCount, +1)}>
										+
									</button>
								</div>
							</div>
						</>
					) : (
						<span>TBD</span>
					)}
				</div>

				{
					match.matchResult.fullTime
						? (
							<>
								<div className='score-wrapper'>
									<div className='score-wrapper-actual'>
										<span className='score-home-team'>
											{match.matchResult.fullTime.homeTeamScore}
										</span>
										|
										<span className='score-away-team'>
											{match.matchResult.fullTime.awayTeamScore}
										</span>
									</div>
								</div>
							</>
						)
						: (
							<span>? : ?</span>
						)
				}
				<div>
					<button onClick={sendRequest}>
						save
					</button>
				</div>

				<div className='team-wrapper'>
					{match.teamAway ? (
						<>
							<div className='team-icon'>
								<img src={match.teamAway.emblemUrl} alt='Team Emblem' />
							</div>
							<div className='team-name'>{match.teamAway.shortName}</div>
							<div className='prediction-wrapper text-4xl'>
								<div>
									<button
										onClick={() =>
											updatePredictionValue(awayPredictedCount, setAwayPredictedCount, -1)}>
										-
									</button>
								</div>
								<div>{awayPredictedCount}</div>
								<div>
									<button
										onClick={() =>
											updatePredictionValue(awayPredictedCount, setAwayPredictedCount, +1)}>
										+
									</button>
								</div>
							</div>
						</>
					) : (
						<span>TBD</span>
					)}
				</div>

			</div>

			{popupMessage && <Popup message={popupMessage} isError={isError} duration={2000} />}
		</>
	)
}