import { AxiosError } from 'axios'
import { format } from 'date-fns'
import { useState } from 'react'
import { PiMinusBold, PiPlusBold } from 'react-icons/pi'
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
			<div className='match-wrapper'>

				<div className='started-time'>
					<div className='text-sm font-normal'>{formattedStartDate}</div>
				</div>

				<div className='match-content'>

					<div className='team-item first'>
						{match.teamHome ? (
							<>
								<div className='team-icon'>
									<img src={match?.teamHome?.emblemUrl} alt='Team Emblem' />
								</div>
								<div className='team-name'>
									{match?.teamHome?.shortName}
								</div>
								<div className='wrapper-prediction'>
									<div className='prediction-value'>{homePredictedCount}</div>
									<div className='flex space-x-4'>
										<div
											className='prediction'
											onClick={() =>
												updatePredictionValue(homePredictedCount, setHomePredictedCount, -1)}>
											<PiMinusBold />
										</div>
										<div
											className='prediction'
											onClick={() =>
												updatePredictionValue(homePredictedCount, setHomePredictedCount, +1)}>
											<PiPlusBold />
										</div>
									</div>
								</div>
							</>
						) : (
							<>
								<div className='text-4xl font-normal'>
									TBD
								</div>
							</>
						)}
					</div>

					<div className='center-item text-5xl font-normal'>
						{match.matchResult?.fullTime
							? (
								<>
									{match.matchResult.fullTime.homeTeamScore} : {match.matchResult.fullTime.awayTeamScore}
								</>
							)
							: (
								<>0 : 0</>
							)
						}
					</div>

					<div className='team-item last'>
						{match.teamAway ? (
							<>
								<div className='team-icon'>
									<img src={match?.teamAway?.emblemUrl} alt='Team Emblem' />
								</div>
								<div className='team-name'>
									{match?.teamAway?.shortName}
								</div>
								<div className='wrapper-prediction'>
									<div className='prediction-value'>{awayPredictedCount}</div>
									<div className='flex space-x-4'>
										<div
											className='prediction'
											onClick={() =>
												updatePredictionValue(awayPredictedCount, setAwayPredictedCount, -1)}>
											<PiMinusBold />
										</div>
										<div
											className='prediction'
											onClick={() =>
												updatePredictionValue(awayPredictedCount, setAwayPredictedCount, +1)}>
											<PiPlusBold />
										</div>
									</div>
								</div>
							</>
						) : (
							<div className='text-4xl font-normal'>
								TBD
							</div>
						)}
					</div>

				</div>

				{match.teamHome && match.teamAway ? (
					<div
						className='prediction-button font-thin'
						onClick={sendRequest}>
						make a prediction
					</div>
				) : (
					<></>
				)}

			</div>

			{popupMessage && <Popup message={popupMessage} isError={isError} duration={2000} />}
		</>
	)
}