import { GiBearFace } from 'react-icons/gi'
import { IUserProfileResponse } from '../../../services/interfaces/Responses/IUserProfileResponse'

interface Props {
	userProfile: IUserProfileResponse | null
}

export function UserProfile({ userProfile }: Props) {
	return (
		<div className="flex items-center space-x-2">
			<div className="p-1 rounded-lg">
				<GiBearFace size={25} />
			</div>
			<div>
				<p className="text-sm">{userProfile?.userName}</p>
			</div>
		</div>
	)
}