<template>
  <header>
    <div class="text-box">
      <span class="text-box__title">
        {{ headerData.title }}
      </span>
    </div>

    <div class="right-side">
      <SearchBar
        v-if="routeMeta.searchBar"
        v-model="searchStore.searchBarValue"
        style="max-width: min(100%, 300px)"
      />

      <v-icon
        v-if="display.smAndDown.value"
        size="24"
        @click="globalStore.navigationOpened = !globalStore.navigationOpened"
      >
        mdi-menu
      </v-icon>
    </div>
  </header>
</template>
<script lang="ts" setup>
import router from '@/router/router';
import SearchBar from './SearchBar.vue';
import { useSearchStore } from '@/stores/search.store';
import { computed } from 'vue';
import { useGlobalStore } from '@/stores/global.store';
import { useDisplay } from 'vuetify';

const searchStore = useSearchStore();
const globalStore = useGlobalStore();

const display = useDisplay();

const currentRoute = computed(() => router.currentRoute.value);
const routeMeta = computed(() => currentRoute.value.meta);

const headerData = computed(() => {
  return {
    title: currentRoute.value.name,
  };
});
</script>
<style lang="scss" scoped>
header {
  display: flex;
  align-items: center;
  justify-content: space-between;
  margin-bottom: 20px;
  gap: 16px;
  min-height: 40px;
  .text-box {
    display: flex;
    flex-direction: column;
    overflow: hidden;
    &__title {
      font-size: 20px;
      font-weight: 600;
      line-height: 25px;
    }
  }
  .right-side {
    display: flex;
    align-items: center;
    justify-content: flex-end;
    flex-grow: 1;
    gap: 4px;
  }
}
</style>
