import { DateTime } from 'luxon';

export class BookingFilter {
  searchQuery = '';
  dateFrom = DateTime.utc().startOf('year');
  dateTo = DateTime.utc().endOf('year').minus({ days: 1 });
}
