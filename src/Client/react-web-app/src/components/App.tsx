import { useEffect, useState } from 'react'
import { Route, Routes } from 'react-router'
import { Layout } from '../layouts/MainLayout'
import { MatchesPage } from '../pages/MatchesPage/MatchesPage'
import { PredictionPage } from '../pages/PredictionPage/PredictionPage'
import { SettingsPage } from '../pages/SettingsPage/SettingsPage'
import { TablePage } from '../pages/TablePage/TablePage'
import { TeamPage } from '../pages/TeamPage/TeamPage'
import { getInitData } from '../services/InitDataService'
import { IInitDataResponse } from '../services/interfaces/IInitDataResponse'
import './App.css'
import { Menu } from './Menu/Menu'
import { UserProfile } from './UserProfile/UserProfile'

export function App() {
  const [data, setData] = useState<IInitDataResponse>({
    isLoading: true,
    userProfile: null,
    matches: null
  })

  useEffect(() => {
    async function fetchData() {
      setData((await getInitData(2)))
    }
    fetchData()
  }, [])

  return (
    <div className='bg-black flex justify-center'>
      <div className='w-full text-white h-screen font-bold flex flex-col max-w-xl'>
        {
          data.isLoading
            ? (
              <p>loading ...</p>
            ) : (
              <>
                <UserProfile userName={data.userProfile?.userName} />

                <Routes>
                  <Route path="/" element={<Layout />}>
                    <Route index element={<MatchesPage matches={data.matches} />} />
                    <Route path='matches' element={<MatchesPage matches={data.matches} />} />
                    <Route path='prediction' element={<PredictionPage />} />
                    <Route path='settings' element={<SettingsPage />} />
                    <Route path='table' element={<TablePage />} />
                    <Route path='team' element={<TeamPage />} />
                  </Route>
                </Routes>

                <Menu />
              </>
            )
        }
      </div>
    </div>
  )
}
