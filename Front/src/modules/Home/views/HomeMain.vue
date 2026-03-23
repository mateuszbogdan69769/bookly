<template>
  <div class="home-view">
    <template v-if="!loading">
      <v-container class="py-8">
        <v-row justify="center" align="center" dense>
          <v-col cols="12" sm="6" md="3" v-for="(stat, index) in stats" :key="index">
            <v-card class="pa-4" elevation="4" rounded="xl">
              <v-card-title class="text-h6 font-weight-bold text-center">
                {{ stat.title }}
              </v-card-title>

              <v-card-text class="text-center">
                <div class="text-h4 font-weight-bold text-primary">{{ stat.value }}</div>
              </v-card-text>
            </v-card>
          </v-col>
        </v-row>
      </v-container>
    </template>

    <Spinner v-else />
  </div>
</template>
<script lang="ts" setup>
import { Stats } from '@/ApiModels/Stats';
import { computed, onMounted, ref } from 'vue';
import { getStatistics } from '../home.service';
import Spinner from '@/components/Spinner.vue';

const statsData = ref(new Stats());
const loading = ref(false);

const stats = computed(() => [
  { title: 'Rezerwacje ogólnie', value: statsData.value.totalBookings },
  { title: 'Rezerwacje dzisiaj', value: statsData.value.bookingsToday },
  { title: 'Goście ogólnie', value: statsData.value.totalGuests },
  { title: 'Goście dzisiaj', value: statsData.value.peoplesToday },
]);

async function loadStats(): Promise<void> {
  setTimeout(() => {
    loading.value = false;
  }, 4000);

  loading.value = true;
  statsData.value = await getStatistics();
  loading.value = false;
}

onMounted(loadStats);
</script>
<style lang="scss" scoped>
.home-view {
  display: flex;
  flex-direction: column;
  overflow: auto;
}
</style>
