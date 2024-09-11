interface Props {
	title: string
	isSelected: boolean
	onClick: () => void
}

export function HorizontalCard({ title, isSelected, onClick }: Props) {
	return (
		<div className={`flex-shrink-0 p-2 m-1 w-44 text-center shadow-lg rounded-lg ${isSelected ? 'bg-green-500' : 'bg-[#9c9c9c]'}`} onClick={onClick}>
			{getStageDisplayValue(title)}
		</div>
	)
}

const stageMapping: Record<string, string> = {
	LEAGUE_STAGE: 'League Stage',
	PLAYOFFS: 'Playoff Stage',
	LAST_16: '1/8 Stage',
	QUARTER_FINALS: '1/4 Stage',
	SEMI_FINALS: 'Semifinal',
	FINAL: 'Final'
}

const getStageDisplayValue = (stage: string): string => {
	return stageMapping[stage] || stage
}