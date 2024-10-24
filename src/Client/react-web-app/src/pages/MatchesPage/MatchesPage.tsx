import { useEffect, useRef, useState } from 'react'
import { Competition } from '../../components/Competition/Competition'
import { HorizontalCard } from '../../components/Match/HorizontalCard'
import { MatchCard } from '../../components/Match/MatchCard'
import { ICompetitionResponse } from '../../services/interfaces/Responses/ICompetitionResponse'
import { IMatchResponse } from '../../services/interfaces/Responses/IMatchResponse'
import { IPredictionResponse } from '../../services/interfaces/Responses/IPredictionResponse'
import './MatchesPage.css'

interface Props {
	competition: ICompetitionResponse | null
	matches: IMatchResponse[] | null
	predictions: IPredictionResponse[] | null
}

export function MatchesPage({ competition, matches, predictions }: Props) {

	const groupedMatchData: Record<string, IMatchResponse[]> = (matches || []).reduce((groups, item) => {
		const stage = item.stage ?? ''
		if (!groups[stage]) {
			groups[stage] = []
		}
		groups[stage].push(item)
		return groups
	}, {} as Record<string, IMatchResponse[]>)

	const activeStage = getActiveStage(groupedMatchData)
	const [selectedCardId, setSelectedCardId] = useState<string | null>(activeStage)
	const [selectedItems, setSelectedItems] = useState<IMatchResponse[]>(
		() => groupedMatchData[activeStage] || []
	)
	const [showFinished, setShowFinished] = useState<boolean>(() => {
		const saved = localStorage.getItem('showFinishedMatches')
		return saved !== null ? JSON.parse(saved) : false
	})

	const matchCardRef = useRef<HTMLDivElement>(null)

	const scrollToMatchCard = (offset: number) => {
		if (matchCardRef.current) {
			const elementPosition = matchCardRef.current.getBoundingClientRect().top + window.pageYOffset
			const offsetPosition = elementPosition - offset

			window.scrollTo({
				top: offsetPosition,
				behavior: 'smooth',
			})
		}
	}

	const handleCardClick = (items: IMatchResponse[], stage: string) => {
		setSelectedCardId(stage)
		setSelectedItems(items)

		const scrollOffset = 90
		scrollToMatchCard(scrollOffset)
	}

	const toggleSwitch = () => {
		setShowFinished(!showFinished)
	}

	const filteredItems = showFinished
		? selectedItems
		: selectedItems.filter(match => match.matchResult?.status !== 'Finished')

	useEffect(() => {
		localStorage.setItem('showFinishedMatches', JSON.stringify(showFinished))
	}, [showFinished])

	return (
		<>
			<h1 className='title-page'>Matches</h1>

			<Competition competition={competition} />

			<div
				className='flex items-center space-x-2 my-6'
				onClick={toggleSwitch}>
				<div className={`toggle-finish-matches ${showFinished ? 'toggle-active-color' : 'toggle-disable-color'}`}>
					<div className={`toggle-finish-circle ${showFinished ? 'translate-x-5' : 'translate-x-0'}`}>
					</div>
				</div>
				<label className='text-sm font-medium'>Show Finished Matches</label>
			</div>

			<div className='w-full overflow-x-auto sticky z-50'>
				<div className='flex space-x-2'>
					{Object.entries(groupedMatchData).map(([stage, items]) => (
						<div key={stage}>
							<HorizontalCard
								isSelected={stage === selectedCardId}
								title={stage}
								onClick={() => handleCardClick(items, stage)}
							/>
						</div>
					))}
				</div>
			</div>

			<div ref={matchCardRef}>
				{filteredItems &&
					filteredItems.map((match: IMatchResponse, index: number) => {
						const maxMatchDay = filteredItems[filteredItems.length - 1].matchDay
						const currentMatchDay = match.matchDay
						const previousMatchDay = index > 0 ? filteredItems[index - 1].matchDay : null

						return (
							<>
								{previousMatchDay !== currentMatchDay && (
									<div className='mt-6 mb-3'>
										<h2>Matchday {currentMatchDay} of {maxMatchDay}</h2>
									</div>
								)}

								<MatchCard
									key={index}
									match={match}
									prediction={predictions?.find(x => x.matchId === match.matchId) ?? null}
								/>
							</>
						)
					})}
			</div>
		</>
	)
}

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