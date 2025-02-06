import { useEffect, useState } from 'react'
import { useDispatch } from 'react-redux'
import { Route, Routes } from 'react-router'
import { Layout } from '../layouts/MainLayout'
import { InfoPage } from '../pages/InfoPage/InfoPage'
import { LeaderboardPage } from '../pages/LeaderboardPage/LeaderboardPage'
import { MatchesPage } from '../pages/MatchesPage/MatchesPage'
import { PredictionDetailPage } from '../pages/PredictionDetailPage/PredictionDetailPage'
import { PredictionPage } from '../pages/PredictionPage/PredictionPage'
import { SettingsPage } from '../pages/SettingsPage/SettingsPage'
import { TeamPage } from '../pages/TeamPage/TeamPage'
import { getInitData } from '../services/InitDataService'
import { IInitDataResponse } from '../services/interfaces/Responses/IInitDataResponse'
import { setInitialUserProfile } from '../store/userProfileSlice'
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
    leaderboard: null
  })

  const dispatch = useDispatch()

  useEffect(() => {
    const fetchData = async () => {
      const response = await getInitData()
      setData(response)

      if (response.userProfile) {
        dispatch(setInitialUserProfile({ isEnabledNotification: response.userProfile.isEnabledNotification }))
      }
    }

    fetchData()
  }, [dispatch])

  return (
    <>
      <div className='flex justify-center overflow-y-auto'>
        <div className='w-full text-white h-screen font-bold flex flex-col max-w-xl'>
          {
            data.isLoading
              ? (
                <p className="loader-wrapper"><span className="loader">Load&nbsp;ng</span></p>
              ) : (
                <>
                  <HeaderBar userProfile={data.userProfile} />

                  <Routes>
                    <Route path="/" element={<Layout />}>
                      <Route index element={<MatchesPage competition={data.competition} matches={data.matches} predictions={data.predictions} />} />
                      <Route path='matches' element={<MatchesPage competition={data.competition} matches={data.matches} predictions={data.predictions} />} />
                      <Route path="prediction" element={<PredictionPage competition={data.competition} />} />
                      <Route path="prediction/:matchId" element={<PredictionDetailPage />} />
                      <Route path='settings' element={<SettingsPage />} />
                      <Route path='leaderboard' element={<LeaderboardPage competition={data.competition} leaderboard={data.leaderboard} />} />
                      <Route path='team' element={<TeamPage />} />
                      <Route path='info' element={<InfoPage />} />
                    </Route>
                  </Routes>
                </>
              )
          }
        </div>
      </div>
      {!data.isLoading && <Menu />}
    </>
  )
}
