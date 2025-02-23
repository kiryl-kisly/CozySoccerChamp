import { useEffect, useState } from 'react'
import { IoCodeWorking, IoEarthSharp, IoFolder, IoNotifications, IoPerson } from 'react-icons/io5'
import { SettingItem } from '../../components/Setting/SettingItem'
import './SettingsPage.css'

export function SettingsPage() {
	const [isInAppVibrate, setIsInAppVibrate] = useState<boolean>(() => {
		const saved = localStorage.getItem('isInAppVibrate')
		return saved !== null ? JSON.parse(saved) : false
	})

	const [isHideFinishedMatches, setIsHideFinishedMatches] = useState<boolean>(() => {
		const saved = localStorage.getItem('isHideFinishedMatches')
		return saved !== null ? JSON.parse(saved) : false
	})

	useEffect(() => {
		localStorage.setItem('isHideFinishedMatches', JSON.stringify(isHideFinishedMatches))
		localStorage.setItem('isInAppVibrate', JSON.stringify(isInAppVibrate))
	}, [isHideFinishedMatches, isInAppVibrate])

	const toggleIsHideFinishedMatches = () => {
		setIsHideFinishedMatches(!isHideFinishedMatches)
	}
	const toggleIsInAppVibrate = () => {
		setIsInAppVibrate(!isInAppVibrate)
	}

	// return (
	// 	<>
	// 		<h1 className='title-page'>Settings</h1>

	// 		<p className='text-gray-400 font-thin'>IN-APP NOTIFICATIONS</p>


	// 		{/* Включить/выключить haptic */}
	// 		<div
	// 			className='flex justify-between items-center ml-5 mr-5 text-lg'
	// 			onClick={toggleIsInAppVibrate}>
	// 			<div className='absolute left-5 p-2'>In-App Vibrate</div>
	// 			<div className='ml-auto p-2'>
	// 				<div className={`toggle ${isInAppVibrate ? 'toggle-active-color' : 'toggle-disable-color'}`}>
	// 					<div className={`toggle-circle ${isInAppVibrate ? 'translate-x-8' : 'translate-x-0'}`}>
	// 					</div>
	// 				</div>
	// 			</div>
	// 		</div>

	// 		<p className='text-gray-400 font-thin mt-6'>OTHER</p>
	// 		{/* Скрыть/показать завершенные матчи */}
	// 		<div
	// 			className='flex justify-between items-center ml-5 mr-5 text-lg'
	// 			onClick={toggleIsHideFinishedMatches}>
	// 			<div className='absolute left-5 p-2'>Hide Finished Matches</div>
	// 			<div className='ml-auto p-2'>
	// 				<div className={`toggle ${isHideFinishedMatches ? 'toggle-active-color' : 'toggle-disable-color'}`}>
	// 					<div className={`toggle-circle ${isHideFinishedMatches ? 'translate-x-8' : 'translate-x-0'}`}>
	// 					</div>
	// 				</div>
	// 			</div>
	// 		</div>
	// 	</>
	// )

	return (
		<>
			<h1 className='title-page'>Settings</h1>

			<div className="bg-[var(--gray-dark)] rounded-[10px] shadow mb-5 w-full max-w-xl mx-auto">
				<SettingItem icon={IoPerson} title="My Profile" isFirst isLast iconBgColor="#238cf5" toLink={'profile'} />
			</div>

			<div className="bg-[var(--gray-dark)] rounded-[10px] shadow mb-5 w-full max-w-xl mx-auto">
				<SettingItem icon={IoNotifications} title="Notifications" isFirst iconBgColor="#e02828" toLink={'notifications'} />
				<SettingItem icon={IoCodeWorking} title="Haptic" iconBgColor="#23d6f5" toLink={'haptic'} />
				<SettingItem icon={IoEarthSharp} title="Language" isLast iconBgColor="#F5A623" extraInfo="English" toLink={'language'} />
			</div>

			<div className="bg-[var(--gray-dark)] rounded-[10px] shadow w-full max-w-xl mx-auto">
				<SettingItem icon={IoFolder} title="Other Settings" isFirst isLast iconBgColor="#6156d6" toLink={'other'} />
			</div>
		</>
	)
}