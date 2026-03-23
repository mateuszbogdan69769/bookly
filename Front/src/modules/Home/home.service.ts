import { Stats } from '@/ApiModels/Stats';
import { getData } from '@/services/global.service';
import { plainToInstance } from 'class-transformer';

export async function getStatistics(): Promise<Stats> {
  const result = await getData('Statistic', '');
  return plainToInstance(Stats, result);
}
