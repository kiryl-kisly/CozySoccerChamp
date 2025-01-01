import { ILeaderboardResponse } from '../../services/interfaces/Responses/ILeaderboardResponse'
import { IUserProfileResponse } from '../../services/interfaces/Responses/IUserProfileResponse'
import './HeaderBar.css'
import { InfoDetail } from './InfoDetail/InfoDetail'
import { PointInfo } from './PointInfo/PointInfo'
import { UserProfile } from './UserProfile/UserProfile'

interface Props {
	userProfile: IUserProfileResponse | null
	leaderboard: ILeaderboardResponse | undefined | null
}

export function HeaderBar({ userProfile, leaderboard }: Props) {
	return (
		<div className="wrapper-top-bar">

			<UserProfile userProfile={userProfile} />
			<PointInfo leaderboard={leaderboard} />
			<InfoDetail />

		</div>
	)
}