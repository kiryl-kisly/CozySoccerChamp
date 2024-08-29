import { GiBearFace } from 'react-icons/gi'

export function UserProfile({ userName = '' }) {
	return (
		<>
			<div className="px-4 z-1">
				<div className="flex items-center space-x-2 pt-4">
					<div className="p-1 rounded-lg bg-[#1d2025]">
						<GiBearFace size={25} />
					</div>
					<div>
						<p className="text-sm">{userName}</p>
					</div>
				</div>
			</div>
		</>
	)
}