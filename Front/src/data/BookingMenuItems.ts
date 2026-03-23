import { IMenuItem } from '@/interfaces/IMenuItem';
import { BookingRoutesTitles } from './BookingRoutesTitles';

export const BookingMenuItems: IMenuItem[] = [
  {
    title: BookingRoutesTitles.Home,
    to: { name: BookingRoutesTitles.Home },
    iconName: 'mdi-circle'
  },
  {
    title: BookingRoutesTitles.Bookings,
    to: { name: BookingRoutesTitles.Bookings },
    iconName: 'mdi-account'
  },
  {
    title: BookingRoutesTitles.Guests,
    to: { name: BookingRoutesTitles.Guests },
    iconName: 'mdi-star'
  },
  {
    title: BookingRoutesTitles.Statuses,
    to: { name: BookingRoutesTitles.Statuses },
    iconName: 'mdi-cog-outline'
  }
];
