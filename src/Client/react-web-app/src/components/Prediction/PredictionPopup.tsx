import { format } from 'date-fns'
import { FaTimes } from 'react-icons/fa'
import { IMatchResponse } from '../../services/interfaces/Responses/IMatchResponse'
import { IPredictionResponse } from '../../services/interfaces/Responses/IPredictionResponse'

interface Props {
	selectedMatch: IMatchResponse | null
	predictions: IPredictionResponse[] | null
	isVisible: boolean
	onClose: () => void
}

export function PredictionPopup({ selectedMatch, predictions, isVisible, onClose, }: Props) {

	const handleClose = () => {
		document.documentElement.style.overflow = 'auto';
		document.body.style.overflow = 'auto';

		onClose();
	};


	if (selectedMatch == null)
		return

	return (
		<div
			className={`fixed bottom-0 left-0 right-0 bg-[#cccccc5f] backdrop-blur-md p-4 shadow-lg transform transition-transform duration-10000 ease-out rounded-t-[28px] ${isVisible ? 'translate-y-0' : 'translate-y-full'
				}`}
			style={{ height: '75vh', zIndex: 9999 }}
		>
			<button className='absolute top-4 right-4 text-black' onClick={handleClose}>
				<FaTimes size={24} />
			</button>

			<div className='flex flex-col items-center mt-10'>
				{/* Дата */}
				<div className='bg-[var(--black)] shadow-lg text-white px-10 py-1 rounded-lg'>
					{format(new Date(selectedMatch?.startTimeUtc as unknown as string), 'dd MMM yyyy HH:mm')}
				</div>
				{/* Результаты команды */}
				<div className="match-teams-wrapper my-8">
					<div className="teams-item w-1/2">
						<img className="team-logo mr-1 w-12 h-12" src={selectedMatch?.teamHome?.emblemUrl} />
						<div className="team-name text-right flex items-center space-x-2 ml-auto">
						{selectedMatch?.teamHome?.shortName}
						</div>
					</div>
					<div className="result-match mx-3 w-1/5 text-3xl">
						{
						selectedMatch?.matchResult?.status === 'Started' ? (
							<div className='bg-green-500 animate-pulse rounded-lg px-4 m-2 text-white font-normal'>active</div>
						) : (
							<>
								<span>{selectedMatch?.matchResult?.fullTime?.homeTeamScore}</span> : <span>{selectedMatch?.matchResult?.fullTime?.awayTeamScore}</span>
							</>
						)}

					</div>
					<div className="teams-item w-1/2">
						<div className="team-name text-left flex items-center space-x-2 mr-auto">
							{selectedMatch?.teamAway?.shortName}
						</div>
						<img className="team-logo ml-1 w-12 h-12" src={selectedMatch?.teamAway?.emblemUrl} />
					</div>
				</div>

				{/* Таблица результатов */}
				{predictions && predictions.length > 0 ? (
					<>
					<div className='title-sub w-full'><h3 className='font-semibold text-black mt-5 mb-3 text-left'>Predictions:</h3></div>
					<div className='text-white p-1 rounded-lg w-full max-h-52 overflow-y-auto prediction-list'>

						{predictions?.map((prediction, index) => (
							<div key={index}
								className="flex justify-between border-b border-black mb-3">

								<div className="font-thin">{prediction.user?.userName}</div>
								<div className="text-lg">
									{prediction.predictedHomeScore} - {prediction.predictedAwayScore}
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