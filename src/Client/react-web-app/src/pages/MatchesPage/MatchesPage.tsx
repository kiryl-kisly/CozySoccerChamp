import { useState } from 'react'
import { HorizontalCard } from '../../components/Match/HorizontalCard'
import { MatchCard } from '../../components/Match/MatchCard'
import { IMatchResponse } from '../../services/interfaces/Responses/IMatchResponse'
import './MatchesPage.css'

export function MatchesPage({ matches, predictions }) {

	const groupedMatchData = matches.reduce((groups, item) => {
		const stage = item.stage
		if (!groups[stage]) {
			groups[stage] = []
		}
		groups[stage].push(item)
		return groups
	}, {} as Record<string, IMatchResponse[]>)

	const [selectedItems, setSelectedItems] = useState<IMatchResponse[] | null>(null)
	const handleCardClick = (items: IMatchResponse[]) => {
		setSelectedItems(items)
	}

	return (
		<>
			<div className='w-full overflow-x-auto'>
				<div className='flex space-x-2'>
					{Object.entries(groupedMatchData).map(([stage, items]) => (
						<HorizontalCard
							key={stage}
							title={stage}
							onClick={() => handleCardClick(items)}
						/>
					))}
				</div>
			</div>

			{selectedItems && selectedItems.map((match: IMatchResponse, index: number) => (
				<MatchCard key={index} match={match} prediction={predictions?.find(x => x.matchId === match.matchId)} />
			))}
		</>
	)
}