import { Competition } from '../../components/Competition/Competition'
import { ICompetitionResponse } from '../../services/interfaces/Responses/ICompetitionResponse'

interface Props {
	competition: ICompetitionResponse | null
}

export function PredictionPage({ competition }: Props) {
	return (
		<>
			<h1 className='text-white mb-10 text-3xl'>Predictions</h1>

			<Competition competition={competition} />

			<div>
				Coming soon...
			</div>
		</>
	)
}