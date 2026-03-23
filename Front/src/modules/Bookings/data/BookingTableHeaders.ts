import { Booking } from '@/ApiModels/Booking';
import { ITableHeader } from '@/interfaces/ITableHeader';

export const BookingTableHeaders: ITableHeader<Booking>[] = [
  {
    title: 'Status',
    key: 'statusId',
    sortable: false
  },
  {
    title: 'Start',
    key: 'startDate',
    sortable: true
  },
  {
    title: 'Koniec',
    key: 'endDate',
    sortable: false
  },
  {
    title: 'Gość',
    key: 'guest',
    sortable: false
  },
  {
    title: 'Ilość osób',
    key: 'partySize',
    sortable: false
  },
  {
    title: 'Notatka',
    key: 'note',
    sortable: false
  },
  {
    title: 'Data utworzenia',
    key: 'createdAt',
    sortable: true
  },
  {
    title: 'Akcje',
    key: 'actions',
    align: 'center',
    sortable: false
  }
];
