import { GiTrophyCup } from 'react-icons/gi'
import { LiaCoinsSolid } from 'react-icons/lia'
import { IUserProfileResponse } from '../../../services/interfaces/Responses/IUserProfileResponse'

interface Props {
	userProfile: IUserProfileResponse | null
}

export function PointInfo({ userProfile }: Props) {
	return (
		<div className="point-block">
			<div className="wrapper-points-info">

				<div className="place-item">
					<div className="icon-item"><GiTrophyCup size={16} /></div>
					<span>{userProfile?.place ?? 0}</span>
				</div>

				<div className="point-item">
					<div className="icon-item"><LiaCoinsSolid size={16} /></div>
					<span>{userProfile?.points ?? 0}</span>
				</div>

			</div>
		</div>
	)
}