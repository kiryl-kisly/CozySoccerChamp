interface Props {
	options: number[]
	selected: number
	onSelect: (value: number) => void
	disabled?: boolean
}

export function IntervalItem({ options, selected, onSelect, disabled }: Props) {
	return (
		<div className={`flex justify-between items-center px-4 py-[6px] text-sm ${disabled ? 'opacity-50 cursor-not-allowed' : 'cursor-pointer'}`}>
			<div className="absolute text-white font-medium text-lg">Interval</div>
			<div className="ml-auto flex bg-[var(--gray-light)] p-1 rounded-lg">
				{options.map((option) => (
					<div
						key={option}
						className={`px-3 py-1 rounded-md transition-all duration-100 ${selected === option ? 'bg-[var(--green)] text-white' : 'text-gray-300'
							} ${disabled ? 'cursor-not-allowed' : 'cursor-pointer hover:bg-[var(--green)] hover:text-white'}`}
						onClick={disabled ? undefined : () => onSelect(option)} // Блокируем клик, если disabled
					>
						{option} min
					</div>
				))}
			</div>
		</div>
	)
}
