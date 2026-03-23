import { defineStore } from 'pinia';
import { MessageModel } from '@/models/MessageModel';
import { MessageTypeEnum } from '@/enums/MessageTypeEnum';
import { Status } from '@/ApiModels/Status';
import {
  createStatus,
  deleteStatus,
  getStatuses,
  updateStatus,
  updateStatusOrder
} from './status.services';
import { StatusViewModel } from './models/StatusViewModel';

interface IStatusesStoreState {
  statuses: Status[];
}

const baseState = (): IStatusesStoreState => ({
  statuses: []
});

export const useStatusStore = defineStore('status', {
  state: baseState,
  actions: {
    async loadStatuses(): Promise<void> {
      this.statuses = await getStatuses();
    },
    async addStatus(data: StatusViewModel): Promise<void> {
      const msg = new MessageModel(
        MessageTypeEnum.Success,
        'Status utworzony pomyślnie'
      );

      await createStatus(data, msg);

      this.loadStatuses();
    },
    async editStatus(data: StatusViewModel): Promise<void> {
      const msg = new MessageModel(
        MessageTypeEnum.Success,
        'Status został zaktualizowany'
      );

      await updateStatus(data, msg);

      this.loadStatuses();
    },
    async deleteStatus(id: number): Promise<void> {
      const msg = new MessageModel(
        MessageTypeEnum.Success,
        'Status usunięty pomyślnie'
      );

      await deleteStatus(id, msg);

      this.loadStatuses();
    },
    async updateStatusOrder(): Promise<void> {
      const data = this.statuses.map((e) => {
        return { id: e.id, order: e.order };
      });
      const msg = new MessageModel(
        MessageTypeEnum.Success,
        'Kolejność została zaktualizowana'
      );

      await updateStatusOrder(data, msg);
    }
  },
  getters: {
    orderedStatuses(state): Status[] {
      return state.statuses.sort((a, b) => a.order - b.order);
    }
  }
});
