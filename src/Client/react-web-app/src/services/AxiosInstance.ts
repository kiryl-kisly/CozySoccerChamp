import { initInitData } from '@telegram-apps/sdk-react'
import axios, { AxiosInstance } from 'axios'

const baseURL = 'http://localhost:8000/api/'

const axiosClient = (): AxiosInstance => {

  const initData = initInitData()

  const headers = {
    'Content-Type': 'application/json',
    'X-Telegram-User-Id': initData?.user?.id,
  }

  const client = axios.create({
    baseURL: baseURL,
    timeout: 5000,
    headers: headers
  })

  return client
}

export default axiosClient