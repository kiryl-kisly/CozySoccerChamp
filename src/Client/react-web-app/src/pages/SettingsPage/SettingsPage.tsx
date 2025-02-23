import { IoCodeWorking, IoEarthSharp, IoFolder, IoNotifications, IoPerson } from 'react-icons/io5'
import { SettingItem } from '../../components/Setting/SettingItem'
import './SettingsPage.css'

export function SettingsPage() {
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