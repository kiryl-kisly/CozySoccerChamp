import { Route, Routes } from 'react-router'
import { Layout } from '../layouts/MainLayout'
import { MatchesPage } from '../pages/MatchesPage/MatchesPage'
import { PredictionPage } from '../pages/PredictionPage/PredictionPage'
import { SettingsPage } from '../pages/SettingsPage/SettingsPage'
import { TablePage } from '../pages/TablePage/TablePage'
import { TeamPage } from '../pages/TeamPage/TeamPage'
import './App.css'
import { Menu } from './Menu/Menu'
import { UserProfile } from './UserProfile/UserProfile'

export function App() {
  return (
    <div className="bg-black flex justify-center">
      <div className="w-full text-white h-screen font-bold flex flex-col max-w-xl">

        <UserProfile />

        <Routes>
          <Route path="/" element={<Layout />}>
            <Route index element={<MatchesPage />} />
            <Route path='matches' element={<MatchesPage />} />
            <Route path='prediction' element={<PredictionPage />} />
            <Route path='settings' element={<SettingsPage />} />
            <Route path='table' element={<TablePage />} />
            <Route path='team' element={<TeamPage />} />
          </Route>
        </Routes>

        <Menu />

      </div>
    </div>
  )
}

{/* <div className="bg-black flex justify-center">
  <div className="w-full bg-black text-white h-screen font-bold flex flex-col max-w-xl">
    <div className="flex-grow mt-4 bg-[#f3ba2f] rounded-t-[48px] relative top-glow z-0">
      {<div className="absolute top-[2px] left-0 right-0 bottom-0 bg-[#1d2025] rounded-t-[46px]">

      </div>}
    </div>
  </div>
</div> */}
