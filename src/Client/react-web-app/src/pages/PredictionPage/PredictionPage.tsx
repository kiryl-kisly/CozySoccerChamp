import { useEffect, useState } from 'react'
import { Competition } from '../../components/Competition/Competition'
import { PredictionCard } from '../../components/Prediction/PredictionCard'
import { PredictionPopup } from '../../components/Prediction/PredictionPopup'
import { ICompetitionResponse } from '../../services/interfaces/Responses/ICompetitionResponse'
import { IMatchResponse } from '../../services/interfaces/Responses/IMatchResponse'
import { IPredictionResponse } from '../../services/interfaces/Responses/IPredictionResponse'
import { getStartedOrFinished } from '../../services/MatchService'
import { getPredictionsByMatchId } from '../../services/PredictionService'

interface Props {
	competition: ICompetitionResponse | null
	matches: IMatchResponse[] | null
}

export function PredictionPage({ competition, matches }: Props) {
	const [data, setData] = useState<IMatchResponse[] | null>(matches)
	const [selectedMatch, setSelectedMatch] = useState<IMatchResponse | null>(null)
	const [predictions, setPredictions] = useState<IPredictionResponse[] | null>(null)
	const [isPopupVisible, setIsPopupVisible] = useState(false)
	const [showPopup, setShowPopup] = useState(false)

	useEffect(() => {
		async function fetchData() {
			setData(await getStartedOrFinished())
		}
		fetchData()
	}, [])

	useEffect(() => {
		setShowPopup(isPopupVisible)
	}, [isPopupVisible])

	const handleCardClick = async (match: IMatchResponse) => {
		setSelectedMatch(match)
		setIsPopupVisible(true)

		const fetchedPredictions = await getPredictionsByMatchId(match.matchId)
		setPredictions(fetchedPredictions)
	}

	const closePopup = () => {
		setIsPopupVisible(false)
	}

	return (
		<>
			<h1 className='title-page'>Predictions</h1>

			<Competition competition={competition} />

			{data &&
				data.map((match: IMatchResponse, index: number) => (
					<PredictionCard
						key={index}
						match={match}
						onClick={() => handleCardClick(match)}
					/>
				))}

			<PredictionPopup
				selectedMatch={selectedMatch}
				predictions={predictions}
				isVisible={showPopup}
				onClose={closePopup}
			/>
		</>
	)
}