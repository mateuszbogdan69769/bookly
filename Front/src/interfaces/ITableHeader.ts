export interface ITableHeader<T> {
  title: string;
  key: keyof T | 'actions';
  align?: 'start' | 'center' | 'end';
  sortable: boolean;
}
