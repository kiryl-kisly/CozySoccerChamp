import React, { useRef, useState } from 'react'
import { IoIosArrowDown, IoIosArrowForward } from 'react-icons/io'
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
	const horizontalRefs = useRef<Record<string, HTMLDivElement | null>>({})
	const [selectedCardId, setSelectedCardId] = useState<string | null>(activeStage)
	const [selectedItems, setSelectedItems] = useState<IMatchResponse[]>(() => groupedMatchData[activeStage] || [])

	const filteredItems = isHideFinishedMatches()
		? selectedItems.filter(match => match.matchResult?.status !== 'Finished')
		: selectedItems

	const [expandedMatchDay, setExpandedMatchDay] = useState<number | null>(() => {
		const activeMatchDay = getActiveMatchDay(filteredItems)

		return activeMatchDay ? activeMatchDay : filteredItems[filteredItems.length - 1]?.matchDay ?? null
	})

	const handleCardClick = (items: IMatchResponse[], stage: string) => {
		setSelectedCardId(stage)
		setSelectedItems(items)

		if (horizontalRefs.current[stage]) {
			horizontalRefs.current[stage]?.scrollIntoView({
				behavior: 'smooth',
				block: 'nearest',
				inline: 'start'
			})
		}
	}

	const handleToggleMatchDay = (matchDay: number | null) => {
		setExpandedMatchDay(prev => (prev === matchDay ? null : matchDay))
	}

	return (
		<>
			<h1 className='title-page'>Matches</h1>

			<Competition competition={competition} />

			{/* Горизонтальный скролл стейджей */}
			<div className='w-full overflow-x-auto sticky top-24 z-50'>
				<div className='flex space-x-2'>
					{Object.entries(groupedMatchData).map(([stage, items]) => (
						<div
							key={stage}
							ref={(el) => (horizontalRefs.current[stage] = el)}
						>
							<HorizontalCard
								isSelected={stage === selectedCardId}
								title={stage}
								onClick={() => handleCardClick(items, stage)}
							/>
						</div>
					))}
				</div>
			</div>

			<div className='mb-4'>

			</div>

			{/* Карточки с матчами */}
			{filteredItems && filteredItems.map((match: IMatchResponse, index: number) => {
				const maxMatchDay = filteredItems[filteredItems.length - 1].matchDay
				const currentMatchDay = match.matchDay
				const previousMatchDay = index > 0 ? filteredItems[index - 1].matchDay : null
				const activeMatchDay = getActiveMatchDay(filteredItems)

				return (
					<React.Fragment key={index} >
						{previousMatchDay !== currentMatchDay && match.stage !== 'FINAL' && (
							<div
								className={`${expandedMatchDay !== currentMatchDay ? 'toggle-show-match-day' : 'toggle-hide-match-day'} ${(activeMatchDay === match.matchDay && activeStage === match.stage) ? 'text-green-400' : 'text-white'}`
								}
								onClick={() => handleToggleMatchDay(currentMatchDay)}>
								<div className='flex items-center flex-start flex-1 p-4'>
									<span>Matchday {currentMatchDay} of {maxMatchDay}</span>
									<div className='flex items-center ml-auto h-full relative arrow-link'>
										{expandedMatchDay === currentMatchDay
											? <IoIosArrowDown />
											: <IoIosArrowForward />}
									</div>
								</div>
							</div>
						)}

						{(expandedMatchDay === currentMatchDay || match.stage === 'FINAL') && (
							<MatchCard
								key={match.matchId}
								match={match}
								prediction={predictions?.find(x => x.matchId === match.matchId) ?? null}
							/>
						)}
					</React.Fragment>
				)
			})}
		</>
	)
}

const isHideFinishedMatches = (): boolean => {
	const saved = localStorage.getItem('isHideFinishedMatches')
	return saved !== null ? JSON.parse(saved) : false
}

const getActiveMatchDay = (filteredItems: IMatchResponse[]): number | null | undefined => {
	// Добавляем смещение на 12 часов вперёд, чтобы активный matchDay не закрывался сразу
	const adjustedTime = Date.now() + 12 * 60 * 60 * 1000

	return filteredItems.find((match) => new Date(match.startTimeUtc as unknown as string).getTime() > adjustedTime)?.matchDay
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