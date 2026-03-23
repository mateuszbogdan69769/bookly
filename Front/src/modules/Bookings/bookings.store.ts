import { Booking } from '@/ApiModels/Booking';
import { defineStore } from 'pinia';
import {
  createBooking,
  deleteBooking,
  getBookings,
  updateBooking
} from './bookings.services';
import { MessageModel } from '@/models/MessageModel';
import { MessageTypeEnum } from '@/enums/MessageTypeEnum';
import { BookingFilter } from '@/models/BookingFilter';
import { BookingViewModel } from './models/BookingViewModel';

interface IBookingsStoreState {
  bookings: Booking[];
  filter: BookingFilter;
}

const baseState = (): IBookingsStoreState => ({
  bookings: [],
  filter: new BookingFilter()
});

export const useBookingsStore = defineStore('booking', {
  state: baseState,
  actions: {
    async loadBookings(): Promise<void> {
      this.bookings = await getBookings(this.filter);
    },
    async addBooking(data: BookingViewModel): Promise<void> {
      const msg = new MessageModel(
        MessageTypeEnum.Success,
        'Rezerwacja utworzona pomyślnie'
      );

      await createBooking(data, msg);

      this.loadBookings();
    },
    async editBooking(data: BookingViewModel): Promise<void> {
      const msg = new MessageModel(
        MessageTypeEnum.Success,
        'Rezerwacja utworzona zaktualizowana'
      );

      await updateBooking(data, msg);

      this.loadBookings();
    },
    async deleteBooking(id: number): Promise<void> {
      const msg = new MessageModel(
        MessageTypeEnum.Success,
        'Rezerwacja usunięta pomyślnie'
      );

      await deleteBooking(id, msg);

      this.loadBookings();
    }
  },
  getters: {}
});
