import { GiBearFace, GiTrophyCup } from 'react-icons/gi'
import { LiaCoinsSolid } from "react-icons/lia"
import { MdInfo } from "react-icons/md"
import { Link } from 'react-router-dom'
import { ILeaderboardResponse } from '../../services/interfaces/Responses/ILeaderboardResponse'

import { IUserProfileResponse } from '../../services/interfaces/Responses/IUserProfileResponse'
import './UserProfile.css'

interface Props {
	userProfile: IUserProfileResponse | null
	leaderboard: ILeaderboardResponse | undefined | null
}

export function UserProfile({ userProfile, leaderboard }: Props) {
	return (
		<>
			<div className="wrapper-top-bar">
				<div className="user-block flex items-center space-x-2">
					<div className="p-1 rounded-lg bg-[#1d2025]">
						<GiBearFace size={25} />
					</div>
					<div>
						<p className="text-sm">{userProfile?.userName}</p>
					</div>
				</div>
				<div className="point-block">

					<div className="wrapper-points-info">
						<div className="point-item">
							<div className="icon-item"><LiaCoinsSolid size={16} /></div>
							<span>{leaderboard?.points ?? 0}</span></div>
						<div className="place-item">
							<div className="icon-item"><GiTrophyCup size={16} /></div>
							<span>{leaderboard?.place ?? 0}</span>
						</div>
					</div>

				</div>
				<div className="info-block">
					<Link to='/info'>
						<MdInfo size={25} />
					</Link>
				</div>
			</div>
		</>
	)
}