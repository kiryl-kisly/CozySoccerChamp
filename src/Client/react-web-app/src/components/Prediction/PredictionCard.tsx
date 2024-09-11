import { IoIosArrowForward } from 'react-icons/io'
import { IMatchResponse } from '../../services/interfaces/Responses/IMatchResponse'

interface Props {
	match: IMatchResponse | null
}

export function PredictionCard({ match }: Props) {

	return (
		<>
			<div className='flex items-center justify-between mb-4 p-4 w-96 h-16 text-white rounded-lg border border-gray-500'>

				<div className='flex items-center space-x-2'>
					<div className='w-6 h-6 rounded-full'>
						<img src={match?.teamHome?.emblemUrl} alt='Team Emblem' />
					</div>
					<span className='truncate'>{match?.teamHome?.shortName}</span>
				</div>

				<div className='flex items-center space-x-2'>
					<span className='truncate'>{match?.teamAway?.shortName}</span>
					<div className='w-6 h-6rounded-full'>
						<img src={match?.teamAway?.emblemUrl} alt='Team Emblem' />
					</div>
				</div>

				<div className='flex items-center'>
					<IoIosArrowForward />
				</div>
			</div>
		</>
	)

}