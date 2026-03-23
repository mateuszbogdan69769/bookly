import { AuthorizationData } from '@/ApiModels/AuthorizationData';
import { getData, postData } from './global.service';

export async function isEmailTaken(email: string): Promise<boolean> {
  return await getData('Account', `isUsernameTaken/${email}`);
}

export async function login(data: {
  username: string;
  password: string;
}): Promise<AuthorizationData | null> {
  return await postData('Account', 'login', data, undefined, false);
}

export async function register(data: {
  name: string;
  username: string;
  password: string;
}): Promise<boolean> {
  return await postData('Account', 'register', data, undefined, false);
}
