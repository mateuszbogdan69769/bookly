<template>
  <div class="guests-view">
    <div class="guests-view__header pt-2">
      <v-btn color="blue" variant="tonal" @click="addGuest">Dodaj goscia</v-btn>
    </div>

    <div class="guests-view__content">
      <v-data-table
        :headers="GuestsTableHeaders"
        :items="guestsStore.guests"
        :items-per-page="25"
        fixed-header
        no-data-text="Brak gości"
        :loading="globalStore.loading"
      >
        <template #item.createdAt="{ item }">
          <span>{{ DateHelper.GetDisplayDateInCurrentTimeZone(item.createdAt) }}</span>
        </template>

        <template #item.bookings="{ item }">
          {{ item.bookings.length }}
        </template>

        <template #item.actions="{ item }">
          <div class="d-flex justify-center">
            <v-icon
              color="medium-emphasis"
              icon="mdi-pencil"
              size="small"
              class="mr-2"
              @click="editGuest(item.id)"
            />

            <v-icon
              color="medium-emphasis"
              icon="mdi-delete"
              size="small"
              @click="openDeleteDialog(item.id)"
            />
          </div>
        </template>
      </v-data-table>
    </div>
  </div>

  <v-dialog v-model="dialogVisible" max-width="500">
    <v-card
      :subtitle="isEditing ? 'Edycja gościa' : 'Dodaj gościa'"
      :title="isEditing ? 'Edytuj' : 'Dodaj'"
    >
      <template #text>
        <Form ref="guestForm" class="d-flex flex-column ga-3">
          <TextField
            v-model="guestData.name"
            :rules="[$validMsg(guestData.v$.name.required)]"
            label="Imię"
            placeholder="Jan"
          />

          <TextField
            v-model="guestData.surname"
            :rules="[$validMsg(guestData.v$.surname.required)]"
            label="Nazwisko"
            placeholder="Kowalski"
          />
        </Form>
      </template>

      <v-divider></v-divider>

      <v-card-actions class="bg-surface-light">
        <v-btn text="Anuluj" variant="plain" @click="dialogVisible = false" />

        <v-spacer />

        <v-btn :text="isEditing ? 'Edytuj' : 'Zapisz'" @click="handleSaveClick" />
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>
<script lang="ts" setup>
import { onMounted, ref } from 'vue';
import { GuestsTableHeaders } from '../data/GuestsTableHeaders';
import { useGuestsStore } from '../guests.store';
import { watchDebounced } from '@vueuse/core';
import { useSearchStore } from '@/stores/search.store';
import { useGlobalStore } from '@/stores/global.store';
import { DateHelper } from '@/helpers/DateHelper';
import { GuestViewModel } from '../models/GuestViewModel';
import { useConfirmationDialog } from '@/helpers/useConfirmationDialog';
import Form from '@/components/Form.vue';
import TextField from '@/components/TextField.vue';

const guestsStore = useGuestsStore();
const searchStore = useSearchStore();
const globalStore = useGlobalStore();

const isEditing = ref(false);
const dialogVisible = ref(false);

const guestForm = ref<InstanceType<typeof Form>>();

const guestData = ref(new GuestViewModel());

function addGuest(): void {
  isEditing.value = false;
  guestData.value = new GuestViewModel();
  dialogVisible.value = true;
}

function editGuest(id: number): void {
  const guest = guestsStore.guests.find((g) => g.id === id);
  if (!guest) return;

  isEditing.value = true;
  guestData.value.setFromGuest(guest);
  dialogVisible.value = true;
}

async function openDeleteDialog(id: number): Promise<void> {
  const guest = guestsStore.guests.find((b) => b.id === id);
  if (!guest) return;

  const title = 'Usuwanie gościa';
  const message = `Czy na pewno chcesz usunąć gościa "${(guest.name, guest.surname)}"?`;
  const confirmed = await useConfirmationDialog({ title, message });
  if (!confirmed) return;

  await guestsStore.deleteGuest(id);
}

async function handleSaveClick(): Promise<void> {
  const formValid = await guestForm.value.validate();
  if (!formValid) return;

  if (isEditing.value) {
    await guestsStore.editGuest(guestData.value);
  } else {
    await guestsStore.addGuest(guestData.value);
  }

  dialogVisible.value = false;
}

watchDebounced(
  () => searchStore.searchBarValue,
  (value) => {
    guestsStore.loadGuests(value);
  },
  { debounce: 300, maxWait: 500 }
);

onMounted(guestsStore.loadGuests);
</script>
<style lang="scss" scoped>
.guests-view {
  display: flex;
  flex-direction: column;
  gap: 25px;
  flex-grow: 1;
  overflow: hidden;
  &__content {
    flex-grow: 1;
    overflow: hidden;
    :deep .v-data-table {
      flex-grow: 1;
      height: 100%;
      &__td {
        span {
          white-space: nowrap;
        }
      }
    }
  }
}
</style>
