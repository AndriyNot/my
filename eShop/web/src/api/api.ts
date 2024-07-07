import axios from 'axios';
import { EmailRequest} from '../interfaces/email';
import authStore from "../auth/authStore";

export const fetchWithAuth = async (url: string, options: RequestInit = {}) => {
  const token = authStore.user?.access_token;
  if (!token) {
    throw new Error("No access token available");
  }

  const response = await fetch(url, {
    ...options,
    headers: {
      ...options.headers,
      Authorization: `Bearer ${token}`,
    },
  });

  if (!response.ok) {
    throw new Error("Network response was not ok");
  }

  return response.json();
};


export const sendEmail = async (emailRequest: EmailRequest): Promise<void> => {
  await axios.post('https://localhost:32768/api/Order/place-order', emailRequest);
};
