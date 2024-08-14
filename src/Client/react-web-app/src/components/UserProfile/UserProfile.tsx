import { GiBearFace } from "react-icons/gi"

export function UserProfile() {
	return (
		<>
			<div className='relative w-full'>
				<div className='absolute top-0 right-0'>
					<div className='flex items-center space-x-2 text-white font-bold text-sm p-2'>
						<GiBearFace size={25} />
						<p>Lemon Drink</p>
					</div>
				</div>
			</div>
		</>
	)
}