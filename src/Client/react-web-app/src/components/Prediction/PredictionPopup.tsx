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
	if (selectedMatch == null)
		return

	return (
		<div
			className={`fixed bottom-0 left-0 right-0 bg-[#cccccc5f] backdrop-blur-md p-4 shadow-lg transform transition-transform duration-10000 ease-out rounded-t-[28px] ${isVisible ? 'translate-y-0' : 'translate-y-full'
				}`}
			style={{ height: '75vh', zIndex: 9999 }}
		>
			<button className='absolute top-4 right-4 text-black' onClick={onClose}>
				<FaTimes size={24} />
			</button>

			<div className='flex flex-col items-center mt-10'>

				<div className='bg-[var(--black)] shadow-lg text-white px-10 py-1 rounded-lg'>
					{format(new Date(selectedMatch?.startTimeUtc as unknown as string), 'dd MMM yyyy HH:mm')}
				</div>

				<div className='flex justify-center w-full my-3 items-center text-black'>

					<div className='w-1/2 team-col-left'>
						<div className='w-12 h-12 rounded-full'>
							<img src={selectedMatch?.teamHome?.emblemUrl} alt='Team Emblem' />
						</div>
						<span>{selectedMatch?.teamHome?.shortName}</span>
					</div>


					<div className='w-1/4 text-center'>
						{
							selectedMatch?.matchResult?.status === 'Started' ? (
								<div className='bg-green-500 animate-pulse rounded-lg px-4 m-2 text-white font-normal'>active</div>
							) : (
								<div className='text-2xl font-bold'>
									{selectedMatch?.matchResult?.fullTime?.homeTeamScore} - {selectedMatch?.matchResult?.fullTime?.awayTeamScore}
								</div>
							)}
					</div>

					<div className='w-1/2 team-col-right'>
						<span>{selectedMatch?.teamAway?.shortName}</span>
						<div className='w-12 h-12 rounded-full'>
							<img src={selectedMatch?.teamAway?.emblemUrl} alt='Team Emblem' />
						</div>
					</div>
				</div>

				{predictions && predictions.length > 0 ? (
					<div className='text-white p-1 rounded-lg w-full'>
						<h3 className='font-semibold text-black mt-5 mb-3'>Predictions:</h3>
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
				) : (
					<p>No predictions available...</p>
				)}

			</div>
		</div>
	)
}