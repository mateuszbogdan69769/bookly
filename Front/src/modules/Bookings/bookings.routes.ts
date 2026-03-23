import ViewHeader from '@/components/ViewHeader.vue';
import { BookingRoutesTitles } from '@/data/BookingRoutesTitles';

export const BookingsRoutes = [
  {
    name: BookingRoutesTitles.Bookings,
    path: 'bookings',
    components: {
      default: () => import('@/modules/Bookings/views/BookingsView.vue'),
      viewHeader: ViewHeader
    },
    meta: {
      searchBar: true,
      requiresAuthorization: true
    }
  }
];
