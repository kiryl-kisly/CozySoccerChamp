import { AxiosError } from 'axios'
import { format } from 'date-fns'
import { useState } from 'react'
import { IPredictionRequest } from '../../services/interfaces/Requests/IPredictionRequest'
import { IMatchResponse } from '../../services/interfaces/Responses/IMatchResponse'
import { IPredictionResponse } from '../../services/interfaces/Responses/IPredictionResponse'
import { makePrediction } from '../../services/PredictionService'
import { Popup } from '../Popup/Popup'
import './MatchCard.css'

interface Props {
	match: IMatchResponse
	prediction: IPredictionResponse | null
}

export function MatchCard({ match, prediction }: Props) {
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
				matchId: match.matchId ?? 0,
				prediction: { predictedHomeScore: homePredictedCount, predictedAwayScore: awayPredictedCount }
			}

			const result = await makePrediction(request)
			if (result.status === 200 && result.statusText === 'OK') {
				setPopupMessage('Your action was saved successfully')
				setIsError(false)
			}
		} catch (error) {
			const axiosError = error as AxiosError<{ message?: string }>
			const errorMessage = axiosError.response?.data?.message || axiosError.message

			setPopupMessage(errorMessage)
			setIsError(true)
		}
	}

	const formattedStartDate = format(new Date(match?.startTimeUtc as unknown as string), 'dd MMM yyyy HH:mm')

	return (
		<>
			<div className='wrapper '>
				<div className='started-time'>
					<div className='text-sm font-normal'>{formattedStartDate}</div>
				</div>

				<div className='main-content'>



					<div className='team-item first'>
						<div className='team-icon'>
							<img src={match?.teamHome?.emblemUrl} alt='Team Emblem' />
						</div>
						<div className='team-name'>
							{match?.teamHome?.shortName}
						</div>
						<div className='wrapper-prediction'>
							<div className='prediction-value'>0</div>
							<div className='flex space-x-4'>
								<div className='prediction'>-</div>
								<div className='prediction'>+</div>
							</div>
						</div>
					</div>

					<div className='center-item text-5xl font-normal'>
						0 : 0
					</div>

					<div className='team-item last'>
						<div className='team-icon'>
							<img src={match?.teamAway?.emblemUrl} alt='Team Emblem' />
						</div>
						<div className='team-name'>
							{match?.teamAway?.shortName}
						</div>
						<div className='wrapper-prediction'>
							<div className='prediction-value'>0</div>
							<div className='flex space-x-4'>
								<div className='prediction'>-</div>
								<div className='prediction'>+</div>
							</div>
						</div>
					</div>



				</div>

				<div className='prediction-button font-thin'>
					make a prediction
				</div>
			</div>





			{/* <div className='wrapper'>
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
					match.matchResult?.fullTime
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
						make prediction
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

			</div> */}

			{popupMessage && <Popup message={popupMessage} isError={isError} duration={2000} />}
		</>
	)
}