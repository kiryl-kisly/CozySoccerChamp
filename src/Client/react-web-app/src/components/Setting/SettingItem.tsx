import { IoIosArrowForward } from 'react-icons/io'
import { Link } from "react-router-dom"

export const SettingItem = ({
	icon: Icon,
	title,
	extraInfo,
	isLast,
	isFirst,
	iconBgColor,
	toLink
}: {
	icon: any
	title: string
	extraInfo?: string
	isLast?: boolean
	isFirst?: boolean
	iconBgColor: string
	toLink: string
}) => {
	return (
		<Link to={toLink} className="block">
			<div className={`flex justify-between items-center px-4 py-[6px] hover:bg-gray-600 cursor-pointer relative ${isFirst ? 'rounded-t-[10px]' : ''} ${isLast ? 'rounded-b-[10px]' : ''}`}>
				<div
					className='w-8 h-8 flex items-center justify-center text-green-500 rounded-lg mr-4'
					style={{ backgroundColor: iconBgColor }}>
					<Icon className="text-xl text-white" />
				</div>
				<div className='flex-1 flex justify-between items-center'>
					<div className='text-white font-medium text-lg'>{title}</div>

					{extraInfo && <span className="ml-auto text-gray-400 text-normal font-thin">{extraInfo}</span>}

					<div className='flex items-center ml-4 h-full relative arrow-link'>
						<IoIosArrowForward />
					</div>
				</div>

				{!isLast && <div className="absolute bottom-0 left-14 right-0 h-[1px] bg-gray-600 mt-5" />}
			</div>
		</Link>
	)
}
