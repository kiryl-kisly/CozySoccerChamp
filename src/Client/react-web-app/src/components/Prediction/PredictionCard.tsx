import { IoIosArrowForward } from 'react-icons/io'
import { IMatchResponse } from '../../services/interfaces/Responses/IMatchResponse'

import { useState } from 'react'

interface Props {
	match: IMatchResponse | null
	onClick: () => void
}

export function PredictionCard({ match, onClick }: Props) {

	const isActive = match?.matchResult?.status === 'Started'

	const [setClick, setIsActive] = useState(false)

	const handleClick = () => {
		setIsActive(!setClick)

		if (!isActive) {
			document.documentElement.style.overflow = 'hidden'
			document.body.style.overflow = 'hidden'
		} else {
			document.documentElement.style.overflow = 'auto'
			document.body.style.overflow = 'auto' // возвращаем overflow
		}

		onClick()
	}

	return (
		<>
			<div className={`card-prediction flex w-full h-16 mb-4 border-l-8  text-white rounded-lg border ${isActive ? 'border-green-500' : 'border-gray-500'}`}
				onClick={onClick}>

				<div className='flex items-center flex-start flex-1 p-4' onClick={handleClick}>
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
		</>
	)
}