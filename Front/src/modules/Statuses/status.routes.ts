import { BookingRoutesTitles } from '@/data/BookingRoutesTitles';
import ViewHeader from '@/components/ViewHeader.vue';

export const StatusRoutes = [
  {
    name: BookingRoutesTitles.Statuses,
    path: 'statuses',
    components: {
      default: () => import('@/modules/Statuses/views/StatusView.vue'),
      viewHeader: ViewHeader
    },
    meta: {
      requiresAuthorization: true
    }
  }
];
