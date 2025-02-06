import { IUserProfileResponse } from '../../services/interfaces/Responses/IUserProfileResponse'
import './HeaderBar.css'
import { InfoDetail } from './InfoDetail/InfoDetail'
import { PointInfo } from './PointInfo/PointInfo'
import { UserProfile } from './UserProfile/UserProfile'

interface Props {
	userProfile: IUserProfileResponse | null
}

export function HeaderBar({ userProfile }: Props) {
	return (
		<div className="wrapper-top-bar">

			<UserProfile userProfile={userProfile} />
			<PointInfo userProfile={userProfile} />
			<InfoDetail />

		</div>
	)
}