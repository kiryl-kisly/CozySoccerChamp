import { GiTrophyCup } from 'react-icons/gi'
import { LiaCoinsSolid } from 'react-icons/lia'
import { ILeaderboardResponse } from '../../../services/interfaces/Responses/ILeaderboardResponse'

interface Props {
	leaderboard: ILeaderboardResponse | undefined | null
}

export function PointInfo({ leaderboard }: Props) {
	return (
		<div className="point-block">
			<div className="wrapper-points-info">

				<div className="place-item">
					<div className="icon-item"><GiTrophyCup size={16} /></div>
					<span>{leaderboard?.place ?? 0}</span>
				</div>

				<div className="point-item">
					<div className="icon-item"><LiaCoinsSolid size={16} /></div>
					<span>{leaderboard?.points ?? 0}</span>
				</div>

			</div>
		</div>
	)
}