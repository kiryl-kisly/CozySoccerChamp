import { IoIosArrowForward } from 'react-icons/io'
import { IMatchResponse } from '../../services/interfaces/Responses/IMatchResponse'

interface Props {
	match: IMatchResponse
	onClick: () => void
}

export function PredictionCard({ match, onClick }: Props) {
	return (
		<div
			className={`card-prediction flex w-full h-16 mb-4 border-l-8 text-white rounded-lg border ${match?.matchResult?.status === 'Started' ? 'border-green-500' : 'border-gray-500'
				}`}
			onClick={onClick}
		>
			<div className='flex items-center flex-start flex-1 p-4'>
				<div className='flex items-center space-x-2'>
					<div className='w-6 h-6 rounded-full'>
						<img src={match?.teamHome?.emblemUrl} alt='Team Emblem' />
					</div>
					<span className='truncate'>{match?.teamHome?.shortName}</span>
				</div>
				<span className='mx-2'> - </span>
				<div className='flex items-center space-x-2'>
					<span className='truncate'>{match?.teamAway?.shortName}</span>
					<div className='w-6 h-6 rounded-full'>
						<img src={match?.teamAway?.emblemUrl} alt='Team Emblem' />
					</div>
				</div>

				<div className='flex items-center ml-auto h-full relative arrow-link'>
					<IoIosArrowForward />
				</div>
			</div>
		</div>
	)
}
