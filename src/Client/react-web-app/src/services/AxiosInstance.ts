import axios, { AxiosInstance } from 'axios'

const baseURL = 'http://localhost:5001/api/'

const axiosClient = (): AxiosInstance => {
  const headers = {
    'Content-Type':'application/json',
    'Access': 'application/json',
    'withCredentials': false,
  };

  const client = axios.create({
    baseURL: baseURL,
    timeout: 5000,
    headers: headers
  });

  return client;
};

export default axiosClient;