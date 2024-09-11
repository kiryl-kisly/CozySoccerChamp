import { ICompetitionResponse } from '../../services/interfaces/Responses/ICompetitionResponse'

interface Props {
	competition: ICompetitionResponse | null
}

export function Competition({ competition }: Props) {
	const startYear = new Date(competition?.started!).getFullYear()
	const finishYear = new Date(competition?.finished!).getFullYear()
	const season = `Season ${startYear}/${finishYear}`

	return (
		<>
			<div className='pl-4 pb-4 flex flex-col items-start space-y-2'>
				<div className='flex items-center space-x-2'>
					<img className='h-10 w-10 filter invert'
						src={competition?.emblemUrl} alt={competition?.name} />
					<h1 className='text-2xl font-bold text-white'>{competition?.name}</h1>
				</div>

				<p className='text-gray-400 font-thin'>{season}</p>
			</div>

		</>
	)
}