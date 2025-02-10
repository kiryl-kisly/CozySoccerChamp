import { initHapticFeedback } from '@telegram-apps/sdk-react'
import { AxiosError } from 'axios'
import { format } from 'date-fns'
import { useEffect, useState } from 'react'
import { PiMinusBold, PiPlusBold } from 'react-icons/pi'
import { useDispatch } from 'react-redux'
import { IPredictionRequest } from '../../services/interfaces/Requests/IPredictionRequest'
import { IMatchResponse } from '../../services/interfaces/Responses/IMatchResponse'
import { IPredictionResponse } from '../../services/interfaces/Responses/IPredictionResponse'
import { makePrediction } from '../../services/PredictionService'
import { updatePrediction } from '../../store/slices/predictionsSlice'
import { Popup } from '../Popup/Popup'
import './MatchCard.css'

interface Props {
	match: IMatchResponse
	prediction: IPredictionResponse | null
}

export function MatchCard({ match, prediction }: Props) {
	const hapticFeedback = initHapticFeedback()
	const [homePredictedCount, setHomePredictedCount] = useState(prediction?.predictedHomeScore ?? 0)
	const [awayPredictedCount, setAwayPredictedCount] = useState(prediction?.predictedAwayScore ?? 0)
	const [isPredictionChanged, setIsPredictionChanged] = useState(false)

	const dispatch = useDispatch()

	const minPredictionValue = 0
	const maxPredictionValue = 20

	const updatePredictionValue = (
		_value: number,
		setValue: React.Dispatch<React.SetStateAction<number>>,
		change: number
	) => {
		setValue((prevValue) => {
			const newValue = Math.min(maxPredictionValue, Math.max(minPredictionValue, prevValue + change))

			const isHomeMatch = setValue === setHomePredictedCount
			const isAwayMatch = setValue === setAwayPredictedCount

			if (isHomeMatch) {
				setIsPredictionChanged(newValue !== prediction?.predictedHomeScore || awayPredictedCount !== prediction?.predictedAwayScore)
			} else if (isAwayMatch) {
				setIsPredictionChanged(newValue !== prediction?.predictedAwayScore || homePredictedCount !== prediction?.predictedHomeScore)
			}

			hapticFeedback.impactOccurred('medium')

			return newValue
		})
	}

	const [popupMessage, setPopupMessage] = useState<string | null>(null)
	const [isError, setIsError] = useState<boolean>(false)

	const sendRequest = async () => {
		if (
			!isPredictionChanged ||
			(prediction !== null &&
				homePredictedCount === prediction.predictedHomeScore &&
				awayPredictedCount === prediction.predictedAwayScore)
		) {
			return // Если предсказание не изменилось, ничего не делаем
		}

		try {
			setPopupMessage(null)

			const request: IPredictionRequest = {
				matchId: match.matchId ?? 0,
				prediction: { predictedHomeScore: homePredictedCount, predictedAwayScore: awayPredictedCount },
			}

			const result = await makePrediction(request)
			if (result.status === 200 && result.statusText === 'OK') {
				setPopupMessage('Your action was saved successfully')
				setIsError(false)
				setIsPredictionChanged(false)

				dispatch(
					updatePrediction({
						matchId: match.matchId,
						predictedHomeScore: homePredictedCount,
						predictedAwayScore: awayPredictedCount,
						pointPerMatch: prediction?.pointPerMatch ?? 0,
						coefficient: prediction?.coefficient ?? 1.0,
						user: null
					})
				)

				hapticFeedback.notificationOccurred('success')
			}
		} catch (error) {
			const axiosError = error as AxiosError<{ message?: string }>
			const errorMessage = axiosError.response?.data?.message || axiosError.message

			setPopupMessage(errorMessage)
			setIsError(true)

			hapticFeedback.notificationOccurred('error')
		}
	}

	useEffect(() => {
		setIsPredictionChanged(
			homePredictedCount !== prediction?.predictedHomeScore ||
			awayPredictedCount !== prediction?.predictedAwayScore
		)
	}, [match, prediction, homePredictedCount, awayPredictedCount])

	const formattedStartDate = format(new Date(match?.startTimeUtc as unknown as string), 'dd MMM yyyy HH:mm')
	const archiveMatch = match.matchResult?.status === 'Finished'

	return (
		<>
			<div className='match-wrapper'>
				<div className='started-time'>
					<div className='text-sm font-normal'>{formattedStartDate}</div>
				</div>

				<div className={`match-content ${archiveMatch ? 'disable' : ''}`}>
					{archiveMatch ? (
						<div className='match-disabled'>
							<div className='match-disable-content'>
								<div className='title-match-results text-2xl mt-2'>Match results</div>
								<div className='predictions-results-match'>
									{prediction ? (
										<>
											<div className='text-xs'>Your prediction</div>
											<div className='predictions-results-count'>
												<span>{prediction?.predictedHomeScore}</span> : <span>{prediction?.predictedAwayScore}</span>
											</div>
										</>
									) : null}
								</div>
								<div className='match-teams-wrapper'>
									<div className='teams-item w-1/2'>
										<img className='team-logo mr-1 w-12 h-12' src={match.teamHome?.emblemUrl} />
										<div className='team-name text-right flex items-center space-x-2 ml-auto'>
											{match.teamHome?.shortName}
										</div>
									</div>
									<div className='result-match mx-3 w-1/5 text-3xl'>
										<span>{match.matchResult?.fullTime?.homeTeamScore}</span> :{' '}
										<span>{match.matchResult?.fullTime?.awayTeamScore}</span>
									</div>
									<div className='teams-item w-1/2'>
										<div className='team-name text-left flex items-center space-x-2 mr-auto'>
											{match.teamAway?.shortName}
										</div>
										<img className='team-logo ml-1 w-12 h-12' src={match.teamAway?.emblemUrl} />
									</div>
								</div>

								{prediction ? (
									<div className='match-point'>
										<span className='match-point-value'>
											{prediction.pointPerMatch === 0 ? null : '+'}
											{prediction.pointPerMatch}
										</span>
										<span className='match-point-text'>Points</span>
										{prediction.coefficient && prediction.coefficient > 1.0 && <sup>x{prediction.coefficient}</sup>}
									</div>
								) : null}
							</div>
						</div>
					) : (
						<div className='match-info'>
							<div className='team-item first w-1/3 mr-auto'>
								{match.teamHome ? (
									<>
										<div className='team-icon'>
											<img src={match?.teamHome?.emblemUrl} alt='Team Emblem' />
										</div>
										<div className='team-name'>{match?.teamHome?.shortName}</div>
										<div className='wrapper-prediction'>
											<div className='prediction-value'>{homePredictedCount}</div>
											{match.matchResult?.status === 'Timed' ? (
												<div className='flex space-x-4'>
													<div
														className='prediction'
														onClick={() => updatePredictionValue(homePredictedCount, setHomePredictedCount, -1)}
													>
														<PiMinusBold />
													</div>
													<div
														className='prediction'
														onClick={() => updatePredictionValue(homePredictedCount, setHomePredictedCount, +1)}
													>
														<PiPlusBold />
													</div>
												</div>
											) : null}
										</div>
									</>
								) : (
									<div className='text-4xl font-normal'>TBD</div>
								)}
							</div>

							<div className='match-result'>
								{match.matchResult?.status === 'Started' ? (
									<div className='bg-green-500 animate-pulse rounded-lg px-4 m-2 text-white font-normal text-center text-sm'>
										active
									</div>
								) : null}
								<div className='center-item font-normal text-2xl'>
									{match.matchResult?.fullTime ? (
										<>
											{match.matchResult.fullTime.homeTeamScore} : {match.matchResult.fullTime.awayTeamScore}
										</>
									) : (
										<>0 : 0</>
									)}
								</div>
							</div>

							<div className='team-item last w-1/3 ml-auto'>
								{match.teamAway ? (
									<>
										<div className='team-icon'>
											<img src={match?.teamAway?.emblemUrl} alt='Team Emblem' />
										</div>
										<div className='team-name'>{match?.teamAway?.shortName}</div>
										<div className='wrapper-prediction'>
											<div className='prediction-value'>{awayPredictedCount}</div>
											{match.matchResult?.status === 'Timed' ? (
												<div className='flex space-x-4'>
													<div
														className='prediction'
														onClick={() => updatePredictionValue(awayPredictedCount, setAwayPredictedCount, -1)}
													>
														<PiMinusBold />
													</div>
													<div
														className='prediction'
														onClick={() => updatePredictionValue(awayPredictedCount, setAwayPredictedCount, +1)}
													>
														<PiPlusBold />
													</div>
												</div>
											) : null}
										</div>
									</>
								) : (
									<div className='text-4xl font-normal'>TBD</div>
								)}
							</div>
						</div>
					)}

					{match.teamHome && match.teamAway && match.matchResult?.status === 'Timed' ? (
						<div
							className={`prediction-button font-thin ${isPredictionChanged ? 'prediction-button-changed' : ''
								}`}
							onClick={sendRequest}
						>
							make a prediction
						</div>
					) : null}
				</div>

				{popupMessage && <Popup message={popupMessage} isError={isError} duration={2000} />}
			</div>
		</>
	)
}