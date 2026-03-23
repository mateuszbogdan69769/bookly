import { BookingRoutesTitles } from '@/data/BookingRoutesTitles';
import ViewHeader from '@/components/ViewHeader.vue';

export const GuestsRoutes = [
  {
    name: BookingRoutesTitles.Guests,
    path: 'guests',
    components: {
      default: () => import('@/modules/Guests/views/GuestsView.vue'),
      viewHeader: ViewHeader
    },
    meta: {
      searchBar: true,
      requiresAuthorization: true
    }
  }
];
