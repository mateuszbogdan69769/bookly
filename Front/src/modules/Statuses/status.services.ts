import { Status } from '@/ApiModels/Status';
import { MessageModel } from '@/models/MessageModel';
import {
  deleteData,
  getData,
  postData,
  putData
} from '@/services/global.service';
import { StatusViewModel } from './models/StatusViewModel';
import { plainToInstance } from 'class-transformer';

export async function getStatuses(): Promise<Status[]> {
  const result = await getData<Status[]>('Status');
  return plainToInstance(Status, result);
}

export async function createStatus(
  data: StatusViewModel,
  msg: MessageModel
): Promise<void> {
  await postData('Status', '', data, msg);
}

export async function updateStatus(
  data: StatusViewModel,
  msg: MessageModel
): Promise<void> {
  await putData('Status', '', data, msg);
}

export async function updateStatusOrder(
  data: Array<{ id: number; order: number }>,
  msg: MessageModel
): Promise<void> {
  await putData('Status', 'order', { statusesIdsWithOrders: data }, msg);
}

export async function deleteStatus(
  id: number,
  msg: MessageModel
): Promise<void> {
  await deleteData('Status', id.toString(), msg);
}
