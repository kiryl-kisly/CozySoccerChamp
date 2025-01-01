import { MdInfo } from 'react-icons/md'
import { Link } from 'react-router-dom'

export function InfoDetail() {
	return (
		<div className="info-block">
			<Link to='/info'>
				<MdInfo size={25} />
			</Link>
		</div>
	)
}