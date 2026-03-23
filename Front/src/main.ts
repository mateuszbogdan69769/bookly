import 'reflect-metadata';
import { createApp } from 'vue';
import { createPinia } from 'pinia';
import App from './App.vue';
import router from './router/router';
import vuetify from './plugins/vuetify';
import { ValidationHelper } from './helpers/ValidationHelper';

const app = createApp(App);

app.config.globalProperties.$validMsg = ValidationHelper.validateWithMessage;

app.use(createPinia());
app.use(vuetify);
app.use(router);
app.use(router);

app.mount('#app');
