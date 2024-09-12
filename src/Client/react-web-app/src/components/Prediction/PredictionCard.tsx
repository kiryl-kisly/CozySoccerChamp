import { IoIosArrowForward } from 'react-icons/io'
import { IMatchResponse } from '../../services/interfaces/Responses/IMatchResponse'

interface Props {
	match: IMatchResponse | null
	onClick: () => void
}

export function PredictionCard({ match, onClick }: Props) {

	const isActive = match?.matchResult?.status === 'Started'

	return (
		<>
			<div
				className={`flex w-full h-16 mb-4 text-white rounded-lg border ${isActive ? 'border-green-500' : 'border-gray-500'}`}
				onClick={onClick}
			>

				<div className={`h-full w-2 rounded-l-lg ${isActive ? 'bg-green-500' : 'bg-gray-500'}`}></div>

				<div className='flex items-center justify-between flex-1 p-4'>
					<div className='flex items-center space-x-2'>
						<div className='w-6 h-6 rounded-full'>
							<img src={match?.teamHome?.emblemUrl} alt='Team Emblem' />
						</div>
						<span className='truncate'>{match?.teamHome?.shortName}</span>
					</div>

					<div className='flex items-center space-x-2'>
						<span className='truncate'>{match?.teamAway?.shortName}</span>
						<div className='w-6 h-6 rounded-full'>
							<img src={match?.teamAway?.emblemUrl} alt='Team Emblem' />
						</div>
					</div>

					<div className='flex items-center'>
						<IoIosArrowForward />
					</div>

				</div>
			</div>
		</>
	)
}