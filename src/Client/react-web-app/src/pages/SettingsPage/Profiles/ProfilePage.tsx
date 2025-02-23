import { useBackButton } from '@telegram-apps/sdk-react'
import { useEffect } from 'react'
import { GiBearFace } from 'react-icons/gi'
import { useSelector } from 'react-redux'
import { useNavigate } from 'react-router'
import { SettingItem } from '../../../components/Setting/SettingItem'
import { RootState } from '../../../store/store'

export function ProfilePage() {
	const navigate = useNavigate()
	const backButton = useBackButton()

	useEffect(() => {
		backButton.show()
		const handleBackClick = () => navigate(-1)
		backButton.on("click", handleBackClick)

		return () => {
			backButton.hide()
			backButton.off("click", handleBackClick)
		}
	}, [backButton, navigate])
	const username = useSelector((state: RootState) => state.user.username)

	return (
		<>
			<h1 className='title-page'>Profile</h1>

			<div className="bg-[var(--gray-dark)] rounded-[10px] shadow w-full max-w-xl mx-auto">
				<SettingItem icon={GiBearFace} title="Username" isFirst isLast iconBgColor="var(--gray-light)" toLink={'changeUsername'} extraInfo={username} />
			</div>
		</>
	)
}