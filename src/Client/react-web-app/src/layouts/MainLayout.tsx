import { Outlet } from 'react-router-dom'

export function Layout() {
	return (
		<>
			<div className='main-container'>
				<div className='content-wrapper'>
					<div className='px-4 mt-6'>
						<Outlet />
					</div>
				</div>
			</div>
		</>
	)
}