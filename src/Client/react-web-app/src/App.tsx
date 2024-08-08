import { useState } from 'react'
import { FaUserFriends } from "react-icons/fa"
import { FaTableList } from "react-icons/fa6"
import { IoIosSettings } from "react-icons/io"
import { IoFootball } from "react-icons/io5"
import { RiFlashlightFill } from "react-icons/ri"
import './App.css'

type MenuItem = {
  icon: React.ReactNode
  text: string
}

const menuItems: MenuItem[] = [
  { icon: <IoFootball />, text: 'Matches' },
  { icon: <FaTableList />, text: 'Table' },
  { icon: <RiFlashlightFill />, text: 'Prediction' },
  { icon: <FaUserFriends />, text: 'Team' },
  { icon: <IoIosSettings />, text: 'Settings' },
]

export function App() {
  const [activeIndex, setActiveIndex] = useState<number>(0)

  const handleClick = (index: number) => {
    setActiveIndex(index)
  }

  return (
    <div className='container'>
      <p>fdsfdsfsdfsdf</p> <p>fdsfdsfsdfsdf</p> <p>fdsfdsfsdfsdf</p> <p>fdsfdsfsdfsdf</p> <p>fdsfdsfsdfsdf</p> <p>fdsfdsfsdfsdf</p> <p>fdsfdsfsdfsdf</p> <p>fdsfdsfsdfsdf</p> <p>fdsfdsfsdfsdf</p> <p>fdsfdsfsdfsdf</p> <p>fdsfdsfsdfsdf</p> <p>fdsfdsfsdfsdf</p> <p>fdsfdsfsdfsdf</p> <p>fdsfdsfsdfsdf</p> <p>fdsfdsfsdfsdf</p> <p>fdsfdsfsdfsdf</p> <p>fdsfdsfsdfsdf</p> <p>fdsfdsfsdfsdf</p> <p>fdsfdsfsdfsdf</p> <p>fdsfdsfsdfsdf</p> <p>fdsfdsfsdfsdf</p> <p>fdsfdsfsdfsdf</p> <p>fdsfdsfsdfsdf</p> <p>fdsfdsfsdfsdf</p> <p>fdsfdsfsdfsdf</p> <p>fdsfdsfsdfsdf</p> <p>fdsfdsfsdfsdf</p> <p>fdsfdsfsdfsdf</p> <p>fdsfdsfsdfsdf</p> <p>fdsfdsfsdfsdf</p> <p>fdsfdsfsdfsdf</p> <p>fdsfdsfsdfsdf</p> <p>fdsfdsfsdfsdf</p> <p>fdsfdsfsdfsdf</p> <p>fdsfdsfsdfsdf</p>
      <div className='nav-bar-menu'>
        <div className="navigation">
          <ul>
            {menuItems.map((item, index) => (
              <li
                key={index}
                className={`list ${activeIndex === index ? 'active' : ''}`}
                onClick={() => handleClick(index)}
              >
                <a href="#">
                  <span className="icon">{item.icon}</span>
                  <span className="text">{item.text}</span>
                </a>
              </li>
            ))}
            <div className="indicator"></div>
          </ul>
        </div>
      </div>
    </div>

  )
}
