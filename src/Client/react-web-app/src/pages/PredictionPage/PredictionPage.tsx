import { useEffect, useState } from 'react'
import { Competition } from '../../components/Competition/Competition'
import { PredictionPopup } from '../../components/Popup/PredictionPopup'
import { PredictionCard } from '../../components/Prediction/PredictionCard'
import { ICompetitionResponse } from '../../services/interfaces/Responses/ICompetitionResponse'
import { IMatchResponse } from '../../services/interfaces/Responses/IMatchResponse'
import { getStartedOrFinished } from '../../services/MatchService'

interface Props {
	competition: ICompetitionResponse | null
	matches: IMatchResponse[] | null
}

export function PredictionPage({ competition, matches }: Props) {
	const [data, setData] = useState<IMatchResponse[] | null>(matches)

	const [isPopupOpen, setPopupOpen] = useState(false)
	const [popupContent, setPopupContent] = useState('')

	const openPopup = (content) => {
		setPopupContent(content)
		setPopupOpen(true)
	}

	const closePopup = () => {
		setPopupOpen(false)
	}

	useEffect(() => {
		async function fetchData() {
			setData((await getStartedOrFinished()))
		}
		fetchData()
	}, [])

	return (
		<>
			<h1 className='text-white mb-10 text-3xl'>Predictions</h1>

			<Competition competition={competition} />

			{data && data.map((match: IMatchResponse, index: number) => (
				<>
					<div
						key={index}
						onClick={() => openPopup('Ваши данные для попапа')}>
						<PredictionCard
							key={index}
							match={match} />
					</div>
				</>
			))}

			<PredictionPopup isOpen={isPopupOpen} onClose={closePopup} content={popupContent} />
		</>
	)
}