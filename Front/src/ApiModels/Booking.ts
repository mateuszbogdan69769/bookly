import { DateTimeTransformer } from '@/helpers/DateTimeTransformer';
import { Transform, Type } from 'class-transformer';
import { DateTime } from 'luxon';
import { Guest } from './Guest';

export class Booking {
  id = 0;
  @Transform(DateTimeTransformer)
  startDate = DateTime.utc();
  @Transform(DateTimeTransformer)
  endDate = DateTime.utc();
  partySize = 0;
  note = '';
  @Type(() => Guest)
  guest = new Guest();
  @Transform(DateTimeTransformer)
  createdAt = DateTime.utc();
  statusId = 0;
}
