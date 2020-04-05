import axios from 'axios';
import { BASE_URL } from 'react-native-dotenv';

export const API_URL = {
  UPLOAD_BREATH: '/uploadBreath/',
};

export const apiService = axios.create({
  baseURL: BASE_URL,
  timeout: 50000,
});
