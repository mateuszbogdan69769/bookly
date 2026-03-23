import { LinkHelper } from '@/helpers/LinkHelper';
import { MessageHelper } from '@/helpers/MessageHelper';
import { MessageModel } from '@/models/MessageModel';
import { useAccountStore } from '@/stores/account.store';
import { useGlobalStore } from '@/stores/global.store';
import axios from 'axios';
import { instanceToPlain } from 'class-transformer';

function getAuthorizationHeader(): { Authorization: string } {
  const accountStore = useAccountStore();

  return { Authorization: `Bearer ${accountStore.accessToken}` };
}

export async function getData<T>(
  apiRoute: string,
  apiController = ''
): Promise<T> {
  const globalStore = useGlobalStore();

  const apiUrl = LinkHelper.ensureSlashAtEnd(globalStore.apiUrl);
  let url = `${apiUrl}api/${apiRoute}`;

  if (apiController) {
    url += `/${apiController}`;
  }

  const apiResponse = await globalStore.useLoading(async () => {
    try {
      return await axios.get<T>(url, {
        headers: getAuthorizationHeader()
      });
    } catch (e: any) {
      handleError(e);
      throw e;
    }
  });

  return apiResponse.data;
}

export async function postData<RequestType, ReturnType>(
  apiRoute: string,
  apiController: string,
  body?: RequestType,
  message?: MessageModel,
  addToken = true
): Promise<ReturnType> {
  const globalStore = useGlobalStore();

  const apiUrl = LinkHelper.ensureSlashAtEnd(globalStore.apiUrl);
  const url = `${apiUrl}api/${apiRoute}/${apiController}`;

  const postApiResponse = await useGlobalStore().useLoading(async () => {
    try {
      return await axios.post<ReturnType>(url, instanceToPlain(body), {
        headers: addToken ? getAuthorizationHeader() : {}
      });
    } catch (e: any) {
      handleError(e);
      throw e;
    }
  });

  if (message) {
    MessageHelper.addMessage(message);
  }

  return postApiResponse.data;
}

export async function putData<RequestType, ReturnType>(
  apiRoute: string,
  apiController: string,
  body?: RequestType,
  message?: MessageModel
): Promise<ReturnType> {
  const globalStore = useGlobalStore();

  const apiUrl = LinkHelper.ensureSlashAtEnd(globalStore.apiUrl);
  const url = `${apiUrl}api/${apiRoute}/${apiController}`;

  const postApiResponse = await useGlobalStore().useLoading(async () => {
    try {
      return await axios.put<ReturnType>(url, instanceToPlain(body), {
        headers: getAuthorizationHeader()
      });
    } catch (e: any) {
      handleError(e);
      throw e;
    }
  });

  if (message) {
    MessageHelper.addMessage(message);
  }

  return postApiResponse.data;
}

export async function deleteData(
  apiRoute: string,
  apiController: string,
  message?: MessageModel
): Promise<void> {
  const globalStore = useGlobalStore();

  const apiUrl = LinkHelper.ensureSlashAtEnd(globalStore.apiUrl);
  const url = `${apiUrl}api/${apiRoute}/${apiController}`;

  await useGlobalStore().useLoading(async () => {
    try {
      await axios.delete(url, {
        headers: getAuthorizationHeader()
      });
    } catch (e: any) {
      handleError(e);
      throw e;
    }
  });

  if (message) {
    MessageHelper.addMessage(message);
  }
}

export function handleError(e: any, titleFallback = 'Wystąpił błąd'): void {
  const errorMsg = e?.response?.data?.title ?? titleFallback;
  const message = e?.response?.data?.errors['-']?.[0];
  MessageHelper.addErrorMessage(errorMsg, message);
}
