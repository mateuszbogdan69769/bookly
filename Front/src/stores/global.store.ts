import { defineStore } from 'pinia';

interface IGlobalStoreState {
  apiUrl: string;
  pendingRequests: number;
  navigationOpened: boolean;
}

const baseState = (): IGlobalStoreState => ({
  apiUrl: import.meta.env.VITE_API_URL,
  pendingRequests: 0,
  navigationOpened: false
});

export const useGlobalStore = defineStore('global', {
  state: baseState,
  actions: {
    async useLoading<T>(handler: () => Promise<T>) {
      try {
        this.pendingRequests++;
        return await handler();
      } finally {
        this.pendingRequests--;
      }
    }
  },
  getters: {
    loading(): boolean {
      return this.pendingRequests > 0;
    }
  }
});
