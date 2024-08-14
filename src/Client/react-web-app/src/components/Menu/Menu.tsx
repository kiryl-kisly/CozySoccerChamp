import { useState } from 'react'
import { FaUserFriends } from 'react-icons/fa'
import { FaTableList } from 'react-icons/fa6'
import { IoIosSettings } from 'react-icons/io'
import { IoFootball } from 'react-icons/io5'
import { RiFlashlightFill } from 'react-icons/ri'
import { Link } from 'react-router-dom'
import './Menu.css'

interface MenuItem {
  icon: React.ReactNode
  text: string
}

const menuItems: MenuItem[] = [
  { icon: <FaTableList />, text: 'Table' },
  { icon: <RiFlashlightFill />, text: 'Prediction' },
  { icon: <IoFootball />, text: 'Matches' },
  { icon: <FaUserFriends />, text: 'Team' },
  { icon: <IoIosSettings />, text: 'Settings' },
]

export function Menu() {
  const [activeIndex, setActiveIndex] = useState<number>(2)

  const handleClick = (index: number) => {
    setActiveIndex(index)
  }

  return (
    <div className='nav-bar-menu'>
      <div className='navigation'>
        <ul>
          {menuItems.map((item, index) => (
            <li
              key={index}
              className={`list ${activeIndex === index ? 'active' : ''}`}
              onClick={() => handleClick(index)}
            >
              <Link to={item.text.toLowerCase()}>
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