import './ToggleItem.css'

interface Props {
	title: string
	isActive: boolean
	onClick: () => void
	disabled?: boolean
	isLast?: boolean
}

export function ToggleItem({ title, isActive, onClick, disabled, isLast }: Props) {
	return (
		<div
			className={`flex justify-between items-center px-4 py-[6px] text-sm relative ${disabled ? 'opacity-50 cursor-not-allowed' : 'cursor-pointer'
				}`}
			onClick={disabled ? undefined : onClick} // Блокируем onClick, если disabled
		>
			<div className="absolute text-white font-medium text-lg">{title}</div>
			<div className="ml-auto">
				<div className={`toggle ${isActive ? 'toggle-active-color' : 'toggle-disable-color'}`}>
					<div className={`toggle-circle ${isActive ? 'translate-x-8' : 'translate-x-0'}`} />
				</div>
			</div>
			{!isLast && <div className="absolute bottom-0 left-4 right-0 h-[1px] bg-gray-600 mt-5" />}
		</div>
	)
}