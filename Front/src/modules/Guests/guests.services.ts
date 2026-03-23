import { Guest } from '@/ApiModels/Guest';
import {
  deleteData,
  getData,
  postData,
  putData
} from '@/services/global.service';
import { plainToInstance } from 'class-transformer';
import { MessageModel } from '@/models/MessageModel';
import { GuestViewModel } from './models/GuestViewModel';

export async function getGuests(searchQuery: string): Promise<Guest[]> {
  const result = await getData<Guest[]>(`Guest?searchQuery=${searchQuery}`);
  return plainToInstance(Guest, result);
}

export async function createGuest(
  data: GuestViewModel,
  msg: MessageModel
): Promise<void> {
  await postData('Guest', '', data, msg);
}

export async function updateGuest(
  data: GuestViewModel,
  msg: MessageModel
): Promise<void> {
  await putData('Guest', '', data, msg);
}

export async function deleteGuest(
  id: number,
  msg: MessageModel
): Promise<void> {
  await deleteData('Guest', id.toString(), msg);
}
