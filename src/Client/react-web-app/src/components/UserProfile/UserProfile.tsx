import { GiBearFace } from 'react-icons/gi';
import { MdInfo } from "react-icons/md";
import { GiTrophyCup } from "react-icons/gi";
import { Link } from 'react-router-dom';
import { ILeaderboardResponse } from '../../services/interfaces/Responses/ILeaderboardResponse';
import { LiaCoinsSolid } from "react-icons/lia";

import './UserProfile.css'

interface Props {
	leaderBoard:ILeaderboardResponse | undefined
}

export function UserProfile({ leaderBoard }:Props) {
	return (
		<>
			<div className="wrapper-top-bar">
				<div className="user-block flex items-center space-x-2">
					<div className="p-1 rounded-lg bg-[#1d2025]">
						<GiBearFace size={25} />
					</div>
					<div>
						<p className="text-sm">{leaderBoard?.userName}</p>
					</div>
				</div>
				<div className="point-block">

					<div className="wrapper-points-info">
						<div className="point-item">
							<div className="icon-item"><LiaCoinsSolid size={16}/></div>
							<span>{leaderBoard?.points}</span></div>
						<div className="place-item">
							<div className="icon-item"><GiTrophyCup size={16}/></div>
							<span>{leaderBoard?.place}</span>
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