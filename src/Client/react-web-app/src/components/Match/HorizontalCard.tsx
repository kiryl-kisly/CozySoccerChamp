export function HorizontalCard({ title, isSelected, onClick }) {
	return (
		<div className={`flex-shrink-0 p-2 m-1 w-44 text-center shadow-lg rounded-lg ${isSelected ? 'bg-green-500' : 'bg-[#9c9c9cb7]'}`}>
			<button onClick={onClick}>
				{getStageDisplayValue(title)}
			</button>
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