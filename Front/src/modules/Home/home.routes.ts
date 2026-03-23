import { BookingRoutesTitles } from '@/data/BookingRoutesTitles';
import ViewHeader from '@/components/ViewHeader.vue';

export const HomeRoutes = [
  {
    name: BookingRoutesTitles.Home,
    path: 'home',
    components: {
      default: () => import('@/modules/Home/views/HomeMain.vue'),
      viewHeader: ViewHeader
    },
    meta: {
      requiresAuthorization: true
    }
  }
];
