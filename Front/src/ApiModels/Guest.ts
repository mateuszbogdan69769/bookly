import { Transform, Type } from 'class-transformer';
import { Booking } from './Booking';
import { DateTimeTransformer } from '@/helpers/DateTimeTransformer';
import { DateTime } from 'luxon';

export class Guest {
  id = 0;
  name = '';
  surname = '';
  @Transform(DateTimeTransformer)
  createdAt = DateTime.utc();
  @Type(() => Booking)
  bookings: Booking[] | null = null;

  get fullName(): string {
    return `${this.name} ${this.surname}`;
  }
}
