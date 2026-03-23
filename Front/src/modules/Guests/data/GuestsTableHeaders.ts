import { Guest } from '@/ApiModels/Guest';
import { ITableHeader } from '@/interfaces/ITableHeader';

export const GuestsTableHeaders: ITableHeader<Guest>[] = [
  {
    title: 'Imię',
    key: 'name',
    sortable: false
  },
  {
    title: 'Nazwisko',
    key: 'surname',
    sortable: true
  },
  {
    title: 'Data rejestracji',
    key: 'createdAt',
    sortable: false
  },
  {
    title: 'Ilość rezerwacji',
    key: 'bookings',
    sortable: false
  },
  {
    title: 'Akcje',
    key: 'actions',
    align: 'center',
    sortable: false
  }
];
