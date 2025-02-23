import { useBackButton } from '@telegram-apps/sdk-react'
import { format } from 'date-fns'
import { useCallback, useEffect, useState } from 'react'
import { useLocation, useNavigate, useParams } from 'react-router-dom'
import { IMatchResponse } from '../../../services/interfaces/Responses/IMatchResponse'
import { IPredictionResponse } from '../../../services/interfaces/Responses/IPredictionResponse'
import { getPredictionsByMatchId } from '../../../services/PredictionService'
import './PredictionDetailPage.css'

export function PredictionDetailPage() {
	const backButton = useBackButton()
	const navigate = useNavigate()
	const { matchId } = useParams()
	const match = useLocation().state?.match as IMatchResponse | null

	const [predictions, setPredictions] = useState<IPredictionResponse[]>([])

	const handleBackClick = useCallback(() => {
		navigate(-1)
	}, [navigate])

	useEffect(() => {
		backButton.show()
		backButton.on('click', handleBackClick)

		return () => {
			backButton.off('click', handleBackClick)
			backButton.hide()
		}
	}, [backButton, handleBackClick])

	useEffect(() => {
		async function fetchData() {
			setPredictions(await getPredictionsByMatchId(Number(matchId)))
		}
		fetchData()
	}, [matchId])

	return (
		<div className=''>
			<div className='flex flex-col items-center'>

				{/* Дата */}
				<div className='bg-[var(--gray-light)] shadow-lg text-black px-10 py-1 rounded-lg'>
					{format(new Date(match?.startTimeUtc as unknown as string), 'dd MMM yyyy HH:mm')}
				</div>

				{/* Результаты матча */}
				<div className='match-teams-wrapper my-8'>
					<div className='teams-item w-1/2'>
						<img className='team-logo mr-1 w-16 h-16' src={match?.teamHome?.emblemUrl} />
						<div className='team-name text-right flex items-center space-x-2 ml-auto'>
							{match?.teamHome?.shortName}
						</div>
					</div>

					<div className='result-match mx-3 w-1/5'>
						{match?.matchResult?.status === 'Started' ? (
							<div className='bg-green-500 animate-pulse rounded-lg px-4 m-2 text-white font-normal text-3sm'>
								active
							</div>
						) : (
							<div className='text-4xl'>
								<span>{match?.matchResult?.fullTime?.homeTeamScore}</span>:
								<span>{match?.matchResult?.fullTime?.awayTeamScore}</span>
							</div>
						)}
					</div>

					<div className='teams-item w-1/2'>
						<div className='team-name text-left flex items-center space-x-2 mr-auto'>
							{match?.teamAway?.shortName}
						</div>
						<img className='team-logo ml-1 w-16 h-16' src={match?.teamAway?.emblemUrl} />
					</div>
				</div>

				{/* Таблица результатов */}
				{predictions && predictions.length > 0 ? (
					<>
						<div className='title-sub w-full'>
							<h3 className='font-semibold text-[var(--gray-light)] mt-5 mb-3 text-left'>Predictions</h3>
						</div>
						<div className='text-white p-1 rounded-lg w-full'>
							{predictions?.map((prediction, index) => (
								<div
									key={index}
									className='flex items-center justify-between border-b border-black mb-2 py-2 prediction-list'
								>
									<div className='font-thin'>{prediction.user?.userName}</div>
									<div className='flex items-center justify-center text-lg'>
										{prediction.predictedHomeScore} - {prediction.predictedAwayScore}
										{match?.matchResult?.status === 'Finished' && (
											<div
												className={`win-point ${prediction.pointPerMatch === 5
													? 'bg-green-700'
													: prediction.pointPerMatch === 3
														? 'bg-yellow-600'
														: prediction.pointPerMatch === 2
															? 'bg-orange-700'
															: 'bg-red-700'
													}`}
											>
												{prediction.pointPerMatch !== null ? prediction.pointPerMatch * prediction.coefficient! : null}
											</div>
										)}
									</div>
								</div>
							))}
						</div>
					</>
				) : (
					<p>No predictions available...</p>
				)}
			</div>
		</div>
	)
}
