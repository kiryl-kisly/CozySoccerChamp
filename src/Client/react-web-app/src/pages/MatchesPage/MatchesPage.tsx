import { useRef, useState } from 'react'
import { HorizontalCard } from '../../components/Match/HorizontalCard'
import { MatchCard } from '../../components/Match/MatchCard'
import { IMatchResponse } from '../../services/interfaces/Responses/IMatchResponse'
import { IPredictionResponse } from '../../services/interfaces/Responses/IPredictionResponse'
import './MatchesPage.css'

interface Props {
	matches: IMatchResponse[] | null
	predictions: IPredictionResponse[] | null
}

export function MatchesPage({ matches, predictions }: Props) {

	const groupedMatchData: Record<string, IMatchResponse[]> = (matches || []).reduce((groups, item) => {
		const stage = item.stage ?? ''
		if (!groups[stage]) {
			groups[stage] = []
		}
		groups[stage].push(item)
		return groups
	}, {} as Record<string, IMatchResponse[]>)

	const getActiveStage = (groupedMatchData: Record<string, IMatchResponse[]>): string => {
		const now = new Date()
		for (const [stage, matches] of Object.entries(groupedMatchData)) {
			if (matches.length > 0) {
				const startTime = matches[0]?.startTimeUtc
					? new Date(matches[0]?.startTimeUtc as unknown as string)
					: null

				const endTime = matches[matches.length - 1]?.startTimeUtc
					? new Date(matches[matches.length - 1]?.startTimeUtc as unknown as string)
					: null

				if (startTime instanceof Date && endTime instanceof Date) {
					if (now >= startTime && now <= endTime) {
						return stage
					}
				}
			}
		}

		return 'LEAGUE_STAGE'
	}

	const activeStage = getActiveStage(groupedMatchData)
	const [selectedCardId, setSelectedCardId] = useState<string | null>(activeStage)
	const [selectedItems, setSelectedItems] = useState<IMatchResponse[]>(
		() => groupedMatchData[activeStage] || []
	)

	const matchCardContainerRef = useRef<HTMLDivElement>(null)

	const scrollToMatchCardStart = () => {
		if (matchCardContainerRef.current) {
			matchCardContainerRef.current.scrollTo({ top: 0, behavior: 'smooth' })
		}
	}

	const scrollToHorizontalCard = (ref: HTMLDivElement | null) => {
		if (ref) {
			ref.scrollIntoView({ behavior: 'smooth', inline: 'start' })
		}
	}

	const handleCardClick = (items: IMatchResponse[], stage: string) => {
		setSelectedItems(items)
		setSelectedCardId(stage)
		console.log(stage)
	}

	return (
		<>
			{/* Контейнер с HorizontalCard */}
			<div className='w-full overflow-x-auto sticky top-3'>
				<div className='flex space-x-2'>
					{Object.entries(groupedMatchData).map(([stage, items]) => (
						<div
							key={stage}
							ref={el => stage === selectedCardId && scrollToHorizontalCard(el)}
						>
							<HorizontalCard
								isSelected={stage === selectedCardId}
								title={stage}
								onClick={() => {
									handleCardClick(items, stage)
									scrollToMatchCardStart()
								}}
							/>
						</div>
					))}
				</div>
			</div>

			{/* Контейнер с MatchCard */}
			{selectedItems && selectedItems.map((match: IMatchResponse, index: number) => (
				<MatchCard
					key={index}
					match={match}
					prediction={predictions?.find(x => x.matchId === match.matchId) ?? null}
				/>
			))}
		</>
	)
}