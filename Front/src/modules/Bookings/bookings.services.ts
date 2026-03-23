import { Booking } from '@/ApiModels/Booking';
import {
  deleteData,
  getData,
  postData,
  putData
} from '@/services/global.service';
import { plainToInstance } from 'class-transformer';
import { MessageModel } from '@/models/MessageModel';
import { BookingFilter } from '@/models/BookingFilter';
import { BookingViewModel } from './models/BookingViewModel';

export async function getBookings(data: BookingFilter): Promise<Booking[]> {
  const result = await getData<Booking[]>(
    `Booking?filter=${btoa(JSON.stringify(data))}`
  );
  return plainToInstance(Booking, result);
}

export async function createBooking(
  data: BookingViewModel,
  msg: MessageModel
): Promise<void> {
  await postData('Booking', '', data, msg);
}

export async function updateBooking(
  data: BookingViewModel,
  msg: MessageModel
): Promise<void> {
  await putData('Booking', '', data, msg);
}

export async function updateBookingStatus(
  data: { id: number; statusId: number },
  msg: MessageModel
): Promise<void> {
  await putData('Booking', 'status', data, msg);
}

export async function deleteBooking(
  id: number,
  msg: MessageModel
): Promise<void> {
  await deleteData('Booking', id.toString(), msg);
}
