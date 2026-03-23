import { BookingsRoutes } from '@/modules/Bookings/bookings.routes';
import { GuestsRoutes } from '@/modules/Guests/guests.routes';
import { HomeRoutes } from '@/modules/Home/home.routes';
import { StatusRoutes } from '@/modules/Statuses/status.routes';

export const BookingRoutes = [
  ...HomeRoutes,
  ...BookingsRoutes,
  ...GuestsRoutes,
  ...StatusRoutes
];
