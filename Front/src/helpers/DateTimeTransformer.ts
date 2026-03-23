import { TransformationType } from 'class-transformer';
import { DateTime } from 'luxon';

export function DateTimeTransformer({
  value,
  type
}: {
  value: unknown;
  type: TransformationType;
}): string | DateTime | null {
  if (!value) return null;

  if ((value as DateTime).isValid) {
    if (type === TransformationType.PLAIN_TO_CLASS) {
      return value as DateTime;
    }
    return (value as DateTime).toISO();
  }

  if ((value as string).includes('T')) {
    return DateTime.fromISO(value as string, { zone: 'UTC' }).toUTC();
  }

  return DateTime.fromSQL(value as string, { zone: 'UTC' }).toUTC();
}
