export function HorizontalCard({ title, onClick }) {
	return (
		<div className='flex-shrink-0 p-2 m-1 w-44 text-center bg-[#5b5b5b] shadow-lg rounded-lg'>
			<button onClick={onClick}>{getStageDisplayValue(title)}</button>
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