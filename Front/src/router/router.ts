import { createRouter, createWebHistory } from 'vue-router';
import { AccountRoutesTitles } from '@/data/AccountRoutesTitles';
import { BookingRoutesTitles } from '@/data/BookingRoutesTitles';
import { BookingRoutes } from '@/data/booking.routes';
import { setWindowTitle } from '@/helpers/router.helpers';
import { useAccountStore } from '@/stores/account.store';

const defaultRoute = {
  path: '/:pathMatch(.*)*',
  redirect: { name: AccountRoutesTitles.Login }
};

const routes = [
  {
    name: BookingRoutesTitles.Booking,
    path: `/main`,
    component: () => import('@/views/BookingMain.vue'),
    children: BookingRoutes,
    redirect: { name: BookingRoutesTitles.Home }
  },

  // # Account routes
  {
    name: AccountRoutesTitles.Account,
    path: `/account`,
    component: () => import('@/views/AccountMain.vue'),
    children: [
      {
        name: AccountRoutesTitles.Login,
        path: 'login',
        component: () => import('@/modules/Login/views/LoginView.vue')
      },
      {
        name: AccountRoutesTitles.Register,
        path: 'register',
        component: () => import('@/modules/Register/views/RegisterView.vue')
      }
    ],
    redirect: { name: AccountRoutesTitles.Login }
  }
];

const router = createRouter({
  history: createWebHistory('/'),
  routes: [...routes, defaultRoute]
});

let firstLoad = true;

router.beforeEach(async (to, from, next) => {
  const accountStore = useAccountStore();

  if (firstLoad) {
    accountStore.accessToken = localStorage.getItem('token');
    accountStore.isLoggedIn = !!accountStore.accessToken;
    firstLoad = false;
  }

  if (to.meta.requiresAuthorization && !accountStore.accessToken) {
    router.push({ name: AccountRoutesTitles.Login });
    return;
  }

  next();
});

router.afterEach((to) => setWindowTitle(to.name as string));

export default router;
