import './ToggleItem.css'

interface Props {
	title: string
	isActive: boolean
	onClick: () => void
	disabled?: boolean
}

export function ToggleItem({ title, isActive, onClick, disabled }: Props) {
	return (
		<div
			className={`flex justify-between items-center px-4 py-[6px] text-sm ${disabled ? 'opacity-50 cursor-not-allowed' : 'cursor-pointer'
				}`}
			onClick={disabled ? undefined : onClick} // Блокируем onClick, если disabled
		>
			<div className="absolute text-white font-medium text-lg">{title}</div>
			<div className="ml-auto">
				<div className={`toggle ${isActive ? 'toggle-active-color' : 'toggle-disable-color'}`}>
					<div className={`toggle-circle ${isActive ? 'translate-x-8' : 'translate-x-0'}`} />
				</div>
			</div>
		</div>
	)
}