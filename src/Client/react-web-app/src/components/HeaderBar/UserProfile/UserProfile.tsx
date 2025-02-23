import { GiBearFace } from 'react-icons/gi'
import { useSelector } from 'react-redux'
import { RootState } from '../../../store/store'

export function UserProfile() {
	const username = useSelector((state: RootState) => state.user.username)

	return (
		<div className="flex items-center space-x-2">
			<div className="p-1 rounded-lg">
				<GiBearFace size={25} />
			</div>
			<div>
				<p className="text-sm">{username}</p>
			</div>
		</div>
	)
}