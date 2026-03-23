import { DateTime } from 'luxon';

export class DateHelper {
  static GetDateInCurrentTimeZone(date: DateTime): DateTime {
    return date.setZone('Europe/Warsaw');
  }
  static GetDisplayDateInCurrentTimeZone(
    date?: DateTime,
    settings = { showDate: true, showTime: true }
  ): string {
    if (!date) return '-';
    const { showDate, showTime } = settings;
    const localDate = this.GetDateInCurrentTimeZone(date);

    const dateString = localDate.toFormat('dd-MM-yyyy');
    const timeString = localDate.toFormat('HH:mm');

    if (showDate && showTime) {
      return `${dateString} ${timeString}`;
    }

    if (showDate) {
      return `${dateString}`;
    }

    if (showTime) {
      return `${timeString}`;
    }

    return '-';
  }
}
