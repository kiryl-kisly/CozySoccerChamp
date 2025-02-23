import { useEffect, useState } from 'react'
import { FaUserFriends } from 'react-icons/fa'
import { IoIosSettings } from 'react-icons/io'
import { IoFootball } from 'react-icons/io5'
import { MdLeaderboard } from 'react-icons/md'
import { RiFlashlightFill } from 'react-icons/ri'
import { Link, useLocation } from 'react-router-dom'
import './Menu.css'

interface MenuItem {
  icon: React.ReactNode
  text: string
  path: string
}

const menuItems: MenuItem[] = [
  { icon: <MdLeaderboard />, text: 'Leaderboard', path: '/leaderboard' },
  { icon: <RiFlashlightFill />, text: 'Prediction', path: '/prediction' },
  { icon: <IoFootball />, text: 'Matches', path: '/matches' },
  { icon: <FaUserFriends />, text: 'Team', path: '/team' },
  { icon: <IoIosSettings />, text: 'Settings', path: '/settings' },
]

export function Menu() {
  const location = useLocation()
  const [activeIndex, setActiveIndex] = useState<number>(2)

  useEffect(() => {
    const currentIndex = menuItems.findIndex(item => location.pathname.startsWith(item.path))
    if (currentIndex !== -1) {
      setActiveIndex(currentIndex)
    }
  }, [location.pathname])

  return (
    <div className='nav-bar-menu'>
      <div className='navigation'>
        <ul>
          {menuItems.map((item, index) => (
            <li
              key={index}
              className={`list ${activeIndex === index ? 'active' : ''}`}
              onClick={() => setActiveIndex(index)}
            >
              <Link to={item.path}>
                <span className='icon'>{item.icon}</span>
                <span className='text'>{item.text}</span>
              </Link>
            </li>
          ))}
          <div className='indicator'></div>
        </ul>
      </div>
    </div>
  )
}
