import { FaCheck } from 'react-icons/fa6'

interface Props {
	title: string
	isEnabled?: boolean
	isLast?: boolean
}

export function CheckBoxItem({ title, isEnabled, isLast }: Props) {
	return (
		<div
			className={`flex justify-between items-center px-4 py-[6px] relative`}
		>
			<div className="absolute text-white font-medium text-lg">{title}</div>
			<div className="ml-auto">
				<div className='flex items-center h-8 relative arrow-link'>
					{isEnabled && <FaCheck />}
				</div>
			</div>

			{!isLast && <div className="absolute bottom-0 left-4 right-0 h-[1px] bg-gray-600 mt-5" />}
		</div>
	)
}
