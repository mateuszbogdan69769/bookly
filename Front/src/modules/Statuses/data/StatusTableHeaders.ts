import { Status } from '@/ApiModels/Status';
import { ITableHeader } from '@/interfaces/ITableHeader';

export const StatusTableHeaders: ITableHeader<Status>[] = [
  {
    title: 'Nazwa',
    key: 'name',
    sortable: false
  },
  {
    title: 'Kolor',
    key: 'color',
    sortable: false
  },
  {
    title: 'Akcje',
    key: 'actions',
    align: 'center',
    sortable: false
  }
];
