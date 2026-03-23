<template>
  <v-navigation-drawer
    :model-value="!display.smAndDown.value || globalStore.navigationOpened"
    class="bg-deep-purple pt-5"
    theme="dark"
    :permanent="!display.smAndDown.value"
    :class="{ 'static-drawer': !display.smAndDown.value }"
    @update:model-value="updateNavigationOpened"
  >
    <v-list>
      <v-list-item class="text-center">{{ userEmail }}</v-list-item>

      <v-list-item
        v-for="(item, index) in BookingMenuItems"
        :key="index"
        :to="item.to"
        link
      >
        <div class="d-flex align-center">
          <v-icon class="mr-2">{{ item.iconName }}</v-icon>

          <v-list-item-title>
            {{ item.title }}
          </v-list-item-title>
        </div>
      </v-list-item>
    </v-list>

    <template v-slot:append>
      <div class="pa-2">
        <v-btn block @click="showLogoutDialog"> Wyloguj się </v-btn>
      </div>
    </template>
  </v-navigation-drawer>
</template>
<script lang="ts" setup>
import { BookingMenuItems } from '@/data/BookingMenuItems';
import { useConfirmationDialog } from '@/helpers/useConfirmationDialog';
import { useAccountStore } from '@/stores/account.store';
import { useGlobalStore } from '@/stores/global.store';
import { computed } from 'vue';
import { useDisplay } from 'vuetify';

const accountStore = useAccountStore();
const globalStore = useGlobalStore();

const display = useDisplay();

const userEmail = computed(() => {
  const token = accountStore.accessToken.split('.')[1];
  const data = JSON.parse(atob(token));

  return data.email;
});

function updateNavigationOpened(value: boolean): void {
  if (!display.smAndDown.value) return;
  globalStore.navigationOpened = value;
}

async function showLogoutDialog(): Promise<void> {
  const title = 'Czy na pewno chcesz się wylogować?';
  const confirmed = await useConfirmationDialog({ title });
  if (!confirmed) return;

  accountStore.logout();
}
</script>
<style lang="scss" scoped>
.static-drawer {
  position: static !important;
}
</style>
