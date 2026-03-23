import { defineStore } from 'pinia';

interface ISearchStoreState {
  searchBarValue: string;
}

const baseState = (): ISearchStoreState => ({
  searchBarValue: ''
});

export const useSearchStore = defineStore('search', {
  state: baseState,
  actions: {}
});
