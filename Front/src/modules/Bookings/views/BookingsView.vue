<template>
  <div class="bookings-view">
    <div class="bookings-view__header pt-2">
      <v-btn color="blue" variant="tonal" @click="addBooking">Dodaj rezerwacje</v-btn>

      <div class="d-flex flex-wrap ga-4">
        <DatePicker
          v-model="bookingsStore.filter.dateFrom"
          label="Od"
          @update:modelValue="bookingsStore.loadBookings"
        />

        <DatePicker
          v-model="bookingsStore.filter.dateTo"
          label="Do"
          @update:modelValue="bookingsStore.loadBookings"
        />
      </div>
    </div>

    <div class="bookings-view__content">
      <v-data-table
        :headers="BookingTableHeaders"
        :items="bookingsStore.bookings"
        :items-per-page="25"
        :sort-by="[{ key: 'createdAt', order: 'desc' }]"
        fixed-header
        no-data-text="Brak rezerwacji"
        :loading="globalStore.loading"
      >
        <template #item.statusId="{ item }">
          <div class="d-flex align-center" style="min-width: 140px">
            <StatusSelect
              :model-value="item.statusId"
              plain
              @update:modelValue="updateStatus(item.id, $event)"
            />
          </div>
        </template>

        <template #item.startDate="{ item }">
          <span>{{ DateHelper.GetDisplayDateInCurrentTimeZone(item.startDate) }}</span>
        </template>

        <template #item.endDate="{ item }">
          <span>{{ DateHelper.GetDisplayDateInCurrentTimeZone(item.endDate) }}</span>
        </template>

        <template #item.guest="{ item }">
          <span>{{ item.guest.fullName }}</span>
        </template>

        <template #item.createdAt="{ item }">
          <span>{{ DateHelper.GetDisplayDateInCurrentTimeZone(item.createdAt) }}</span>
        </template>

        <template #item.actions="{ item }">
          <div class="d-flex justify-center">
            <v-icon
              color="medium-emphasis"
              icon="mdi-pencil"
              size="small"
              class="mr-2"
              @click="editBooking(item.id)"
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
      :subtitle="isEditing ? 'Edycja rezerwacji' : 'Dodaj rezerwacje'"
      :title="isEditing ? 'Edytuj' : 'Dodaj'"
    >
      <template #text>
        <Form
          ref="bookingForm"
          class="d-flex flex-column ga-3 pt-1"
          style="overflow: auto; max-height: 100%"
        >
          <TextField
            v-model="bookingData.name"
            :rules="[$validMsg(bookingData.v$.name.required)]"
            label="Imię"
            placeholder="Jan"
          />

          <TextField
            v-model="bookingData.surname"
            :rules="[$validMsg(bookingData.v$.surname.required)]"
            label="Nazwisko"
            placeholder="Kowalski"
          />

          <v-number-input
            v-model="bookingData.partySize"
            :max="20"
            :min="1"
            density="compact"
            variant="outlined"
            label="Ilość gości"
          />

          <div class="d-flex mb-5 ga-5">
            <DatePicker v-model="bookingData.startDate" label="Start rezerwacji" />

            <TimePicker v-model="bookingData.startDate" />
          </div>

          <div class="d-flex mb-5 ga-5">
            <DatePicker v-model="bookingData.endDate" label="Koniec rezerwacji" />

            <TimePicker v-model="bookingData.endDate" />
          </div>

          <div class="d-flex mb-5 ga-5">
            <StatusSelect v-model="bookingData.statusId" />
          </div>

          <v-textarea
            v-model="bookingData.note"
            label="Notatka"
            density="compact"
            variant="outlined"
            hide-details
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
import DatePicker from '../../../components/DatePicker.vue';
import TimePicker from '../../../components/TimePicker.vue';
import { onMounted, ref, watch } from 'vue';
import { useBookingsStore } from '../bookings.store';
import { BookingTableHeaders } from '../data/BookingTableHeaders';
import { DateHelper } from '@/helpers/DateHelper';
import { BookingViewModel } from '../models/BookingViewModel';
import { useConfirmationDialog } from '@/helpers/useConfirmationDialog';
import TextField from '@/components/TextField.vue';
import { useSearchStore } from '@/stores/search.store';
import { watchDebounced } from '@vueuse/core';
import { useGlobalStore } from '@/stores/global.store';
import { BookingFilter } from '@/models/BookingFilter';
import Form from '@/components/Form.vue';
import { GlobalHelper } from '@/helpers/GlobalHelper';
import StatusSelect from '@/components/StatusSelect.vue';
import { updateBookingStatus } from '../bookings.services';
import { MessageTypeEnum } from '@/enums/MessageTypeEnum';
import { MessageModel } from '@/models/MessageModel';

const bookingsStore = useBookingsStore();
const searchStore = useSearchStore();
const globalStore = useGlobalStore();

const isEditing = ref(false);
const dialogVisible = ref(false);

const bookingForm = ref<InstanceType<typeof Form>>();

const bookingData = ref(new BookingViewModel());

function addBooking(): void {
  isEditing.value = false;
  bookingData.value = new BookingViewModel();
  dialogVisible.value = true;
}

function editBooking(id: number): void {
  const booking = bookingsStore.bookings.find((b) => b.id === id);
  if (!booking) return;

  isEditing.value = true;
  bookingData.value.setFromBooking(booking);
  dialogVisible.value = true;
}

async function openDeleteDialog(id: number): Promise<void> {
  const booking = bookingsStore.bookings.find((b) => b.id === id);
  if (!booking) return;

  const title = 'Usuwanie rezerwacji';
  const message = `Czy na pewno chcesz usunąć rezerwację "${booking.guest.fullName}"?`;
  const confirmed = await useConfirmationDialog({ title, message });
  if (!confirmed) return;

  await bookingsStore.deleteBooking(id);
}

async function handleSaveClick(): Promise<void> {
  const formValid = await bookingForm.value.validate();
  if (!formValid) return;

  if (isEditing.value) {
    await bookingsStore.editBooking(bookingData.value);
  } else {
    await bookingsStore.addBooking(bookingData.value);
  }

  dialogVisible.value = false;
}

async function updateStatus(bookingId: number, statusId: number): Promise<void> {
  const msg = new MessageModel(
    MessageTypeEnum.Success,
    'Status został zaktualizowany pomyślnie'
  );
  await updateBookingStatus({ id: bookingId, statusId }, msg);
  bookingsStore.loadBookings();
}

watch(
  () => [bookingData.value.startDate, bookingData.value.endDate],
  ([start, end]) => {
    if (start > end) {
      bookingData.value.endDate = GlobalHelper.deepCopy(start);
    }

    const isSameDay =
      bookingData.value.startDate.toISODate() === bookingData.value.startDate.toISODate();

    if (isSameDay && start.toISO() > end.toISO()) {
      bookingData.value.endDate = start.plus({ hours: 1 });
    }
  }
);

watchDebounced(
  () => searchStore.searchBarValue,
  (v) => {
    bookingsStore.filter.searchQuery = v;
    bookingsStore.loadBookings();
  },
  { debounce: 250, maxWait: 500 }
);

onMounted(() => {
  bookingsStore.filter = new BookingFilter();
  bookingsStore.loadBookings();
});
</script>
<style lang="scss" scoped>
.bookings-view {
  display: flex;
  flex-direction: column;
  gap: 25px;
  flex-grow: 1;
  overflow: hidden;
  &__header {
    display: flex;
    align-items: flex-start;
    justify-content: space-between;
    gap: 15px;
  }
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
