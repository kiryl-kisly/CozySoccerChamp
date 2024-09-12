import { Outlet } from 'react-router-dom'
import { Menu } from '../components/Menu/Menu'

export function Layout() {
	return (
		<>
			<div className='main-container'>
				<div className='content-wrapper'>
					<div className='px-4 py-8'>
						<Outlet />
						<Menu />
					</div>
				</div>
			</div>
		</>
	)
}