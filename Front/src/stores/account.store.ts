import { AccountRoutesTitles } from '@/data/AccountRoutesTitles';
import { BookingRoutesTitles } from '@/data/BookingRoutesTitles';
import { MessageHelper } from '@/helpers/MessageHelper';
import { UserCredentials } from '@/models/UserCredentials';
import router from '@/router/router';
import { isEmailTaken, login, register } from '@/services/account.service';
import { defineStore } from 'pinia';

interface IAccountStoreState {
  isLoggedIn: boolean;
  accessToken: string;
}

const baseState = (): IAccountStoreState => ({
  isLoggedIn: false,
  accessToken: ''
});

export const useAccountStore = defineStore('account', {
  state: baseState,
  actions: {
    async login(data: UserCredentials): Promise<void> {
      const authorizationData = await login({
        username: data.email,
        password: data.password
      });

      if (authorizationData) {
        this.isLoggedIn = true;
        this.accessToken = authorizationData.accessToken;
        localStorage.setItem('token', this.accessToken);
        MessageHelper.addSuccessMessage('Zalogowano pomyślnie');
        await router.push({ name: BookingRoutesTitles.Booking });
      } else {
        MessageHelper.addErrorMessage('Niepoprawny e-mail lub hasło');
      }
    },
    async register(data: UserCredentials): Promise<void> {
      await register({
        name: data.name,
        username: data.email,
        password: data.password
      });

      MessageHelper.addSuccessMessage('Konto utworzone pomyślnie');

      await router.push({ name: AccountRoutesTitles.Login });
    },
    async isEmailTaken(email: string): Promise<boolean> {
      return await isEmailTaken(email);
    },
    async logout(): Promise<void> {
      this.isLoggedIn = false;
      localStorage.removeItem('token');
      await router.push({ name: AccountRoutesTitles.Login });
    }
  },
  getters: {}
});
