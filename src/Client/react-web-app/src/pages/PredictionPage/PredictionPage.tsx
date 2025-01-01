import { useEffect, useState } from 'react'
import { Competition } from '../../components/Competition/Competition'
import { PredictionCard } from '../../components/Prediction/PredictionCard'
import { PredictionPopup } from '../../components/Prediction/PredictionPopup'
import { ICompetitionResponse } from '../../services/interfaces/Responses/ICompetitionResponse'
import { IMatchResponse } from '../../services/interfaces/Responses/IMatchResponse'
import { IPredictionResponse } from '../../services/interfaces/Responses/IPredictionResponse'
import { getStartedOrFinished } from '../../services/MatchService'
import { getPredictionsByMatchId } from '../../services/PredictionService'
import './PredictionPage.css'

interface Props {
	competition: ICompetitionResponse | null
}

export function PredictionPage({ competition }: Props) {
	const [allData, setAllData] = useState<IMatchResponse[]>([])
	const [visibleData, setVisibleData] = useState<IMatchResponse[]>([])
	const [selectedMatch, setSelectedMatch] = useState<IMatchResponse | null>(null)
	const [predictions, setPredictions] = useState<IPredictionResponse[] | null>(null)
	const [isPopupVisible, setIsPopupVisible] = useState(false)
	const [showPopup, setShowPopup] = useState(false)
	const [visibleCount, setVisibleCount] = useState(10)

	useEffect(() => {
		async function fetchData() {
			const data = await getStartedOrFinished()
			setAllData(data)
			setVisibleData(data.slice(0, 10))
		}
		fetchData()
	}, [])

	useEffect(() => {
		setShowPopup(isPopupVisible)
	}, [isPopupVisible])

	const handleLoadMore = () => {
		const newVisibleCount = visibleCount + 10
		setVisibleCount(newVisibleCount)
		setVisibleData(allData.slice(0, newVisibleCount))
	}

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

			<div className='prediction-card-wrapper'>
				{visibleData.map((match: IMatchResponse, index: number) => (
					<PredictionCard
						key={index}
						match={match}
						onClick={() => handleCardClick(match)}
					/>
				))}
			</div>

			{visibleCount < allData.length && (
				<button onClick={handleLoadMore} className='load-more-btn'>
					Load More
				</button>
			)}

			<PredictionPopup
				selectedMatch={selectedMatch}
				predictions={predictions}
				isVisible={showPopup}
				onClose={closePopup}
			/>
		</>
	)
}
