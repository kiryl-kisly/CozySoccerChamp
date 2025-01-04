import { useEffect, useState } from 'react'
import { useNavigate } from 'react-router'
import { Competition } from '../../components/Competition/Competition'
import { PredictionCard } from '../../components/Prediction/PredictionCard'
import { ICompetitionResponse } from '../../services/interfaces/Responses/ICompetitionResponse'
import { IMatchResponse } from '../../services/interfaces/Responses/IMatchResponse'
import { getStartedOrFinished } from '../../services/MatchService'
import './PredictionPage.css'

interface Props {
	competition: ICompetitionResponse | null
}

export function PredictionPage({ competition }: Props) {
	const navigate = useNavigate()
	const [allData, setAllData] = useState<IMatchResponse[]>([])
	const [visibleData, setVisibleData] = useState<IMatchResponse[]>([])
	const [visibleCount, setVisibleCount] = useState(10)

	useEffect(() => {
		async function fetchData() {
			const data = await getStartedOrFinished()
			setAllData(data)
			setVisibleData(data.slice(0, 10))
		}
		fetchData()
	}, [])

	const handleLoadMore = () => {
		const newVisibleCount = visibleCount + 10
		setVisibleCount(newVisibleCount)
		setVisibleData(allData.slice(0, newVisibleCount))
	}

	const handleCardClick = (match: IMatchResponse) => {
		if (match.matchId !== null && match.matchId !== undefined) {
			navigate(`/prediction/${match.matchId}`, { state: { match } })
		}
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
		</>
	)
}
