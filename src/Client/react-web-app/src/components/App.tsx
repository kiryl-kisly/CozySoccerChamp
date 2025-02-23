import { useEffect, useState } from 'react'
import { useDispatch } from 'react-redux'
import { Route, Routes } from 'react-router'
import { Layout } from '../layouts/MainLayout'
import { InfoPage } from '../pages/InfoPage/InfoPage'
import { LeaderboardPage } from '../pages/LeaderboardPage/LeaderboardPage'
import { MatchesPage } from '../pages/MatchesPage/MatchesPage'
import { PredictionDetailPage } from '../pages/PredictionPage/PredictionDetailPage/PredictionDetailPage'
import { PredictionPage } from '../pages/PredictionPage/PredictionPage'
import { HapticPage } from '../pages/SettingsPage/Haptics/HapticPage'
import { LanguagePage } from '../pages/SettingsPage/Languages/LanguagePage'
import { NotificationPage } from '../pages/SettingsPage/Notifications/NotificationPage'
import { OtherPage } from '../pages/SettingsPage/Others/OtherPage'
import { ChangeUserNamePage } from '../pages/SettingsPage/Profiles/ChangeUserNamePage'
import { ProfilePage } from '../pages/SettingsPage/Profiles/ProfilePage'
import { SettingsPage } from '../pages/SettingsPage/SettingsPage'
import { TeamPage } from '../pages/TeamPage/TeamPage'
import { getInitData } from '../services/InitDataService'
import { IInitDataResponse } from '../services/interfaces/Responses/IInitDataResponse'
import { setNotificationSettings } from '../store/slices/notificationSlice'
import { setPredictions } from '../store/slices/predictionsSlice'
import { setUsername } from '../store/slices/userProfileSlice'
import './App.css'
import { HeaderBar } from './HeaderBar/HeaderBar'
import { Menu } from './Menu/Menu'

export function App() {
  const [data, setData] = useState<IInitDataResponse>({
    isLoading: true,
    competition: null,
    userProfile: null,
    matches: null,
    predictions: null,
    leaderboard: null,
  })

  const dispatch = useDispatch()

  useEffect(() => {
    const fetchData = async () => {
      const response = await getInitData()
      setData(response)

      if (response.predictions) {
        dispatch(setPredictions(response.predictions))
      }
      if (response.userProfile?.userName) {
        dispatch(setUsername(response.userProfile.userName))
      }
      if (response.userProfile?.notificationSettings) {
        dispatch(setNotificationSettings(response.userProfile.notificationSettings))
      }
    }

    fetchData()
  }, [dispatch])

  return (
    <>
      <div className='flex justify-center overflow-y-auto'>
        <div className='w-full text-white h-screen font-bold flex flex-col max-w-xl'>
          {data.isLoading ? (
            <p className="loader-wrapper"><span className="loader">Load&nbsp;ng</span></p>
          ) : (
            <>
              <HeaderBar userProfile={data.userProfile} />

              <Routes>
                <Route path="/" element={<Layout />}>
                  <Route index element={<MatchesPage competition={data.competition} matches={data.matches} />} />
                  <Route path='matches' element={<MatchesPage competition={data.competition} matches={data.matches} />} />
                  <Route path='leaderboard' element={<LeaderboardPage competition={data.competition} leaderboard={data.leaderboard} />} />
                  <Route path="prediction" element={<PredictionPage competition={data.competition} />} />
                  <Route path="prediction/:matchId" element={<PredictionDetailPage />} />
                  <Route path='team' element={<TeamPage />} />
                  <Route path='settings' element={<SettingsPage />} />
                  <Route path='settings/profile' element={<ProfilePage />} />
                  <Route path='settings/profile/changeUsername' element={<ChangeUserNamePage />} />
                  <Route path='settings/notifications' element={<NotificationPage />} />
                  <Route path='settings/haptic' element={<HapticPage />} />
                  <Route path='settings/language' element={<LanguagePage />} />
                  <Route path='settings/other' element={<OtherPage />} />

                  <Route path='info' element={<InfoPage />} />
                </Route>
              </Routes>
            </>
          )}
        </div>
      </div>
      {!data.isLoading && <Menu />}
    </>
  )
}