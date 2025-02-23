import { useBackButton } from '@telegram-apps/sdk-react'
import { AxiosError } from 'axios'
import { useEffect, useState } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import { useNavigate } from 'react-router'
import { Popup } from '../../../components/Popup/Popup'
import { InputItem } from '../../../components/Setting/InputItem'
import { changeUserName } from '../../../services/UserProfileService'
import { setUsername } from '../../../store/slices/userProfileSlice'
import { RootState } from '../../../store/store'

export function ChangeUserNamePage() {
	const dispatch = useDispatch()
	const initialUsername = useSelector((state: RootState) => state.user.username)

	const [currentUsername, setCurrentUsername] = useState(initialUsername)
	const [loading, setLoading] = useState(false)
	const [isValid, setIsValid] = useState(true)
	const [popupMessage, setPopupMessage] = useState("")
	const [isError, setIsError] = useState(false)

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

	const isChanged = currentUsername !== initialUsername

	const handleSave = async () => {
		if (!isChanged || !isValid)
			return

		setLoading(true)

		try {
			const response = await changeUserName(currentUsername)

			if (response.status === 200 && response.statusText === "OK") {
				// Обновляем username в Redux
				dispatch(setUsername(currentUsername))

				// Возвращаемся назад
				navigate(-1)
			}
		} catch (error) {
			const axiosError = error as AxiosError<{ message?: string }>
			const errorMessage = axiosError.response?.data?.message || axiosError.message

			setPopupMessage(errorMessage)
			setIsError(true)
		} finally {
			setLoading(false)
		}
	}

	return (
		<>
			<h1 className='title-page'>Username</h1>

			<div className="bg-[var(--gray-dark)] rounded-[10px] shadow w-full max-w-xl mx-auto">
				<InputItem
					initialValue={initialUsername}
					onChange={setCurrentUsername}
					onValidChange={setIsValid}
				/>
			</div>
			<p className="text-gray-400 ml-4 font-thin text-sm mt-2">
				You can choose a username. This nickname will be displayed everywhere in the mini-app: in the top bar, in the prediction table and leaderboard.
			</p>
			<p className="text-gray-400 ml-4 font-thin text-sm mt-5">
				You can use <span className="font-bold">a-z, A-Z, 0-9</span> and underscores. Minimum length is <span className="font-bold">5</span> characters.
			</p>

			{/* Кнопка сохранения */}
			<button
				className={`mt-10 w-full max-w-xs mx-auto block py-2 px-4 text-white rounded-lg transition ${isChanged && isValid
					? "bg-green-500"
					: "bg-gray-500 cursor-not-allowed"
					}`}
				disabled={!isChanged || !isValid || loading}
				onClick={handleSave}
			>
				{loading ? "Saving..." : "Save"}
			</button>

			{popupMessage && <Popup message={popupMessage} isError={isError} duration={2000} />}
		</>
	)
}
