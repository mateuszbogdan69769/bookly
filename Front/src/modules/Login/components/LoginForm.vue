<template>
  <Form ref="loginForm" class="login-form" @submit="handleLogin">
    <v-card class="mx-auto pa-12 pb-8" elevation="8" max-width="448" rounded="lg">
      <div class="text-subtitle-1 text-medium-emphasis">E-mail</div>

      <TextField
        v-model="userCredentials.email"
        :rules="[
          $validMsg(userCredentials.v$.email.required),
          $validMsg(userCredentials.v$.email.valid),
        ]"
        placeholder="Wpisz e-mail"
        prepend-inner-icon="mdi-email-outline"
      />

      <div class="text-subtitle-1 text-medium-emphasis">Hasło</div>

      <TextField
        v-model="userCredentials.password"
        :rules="[
          $validMsg(userCredentials.v$.password.required),
          $validMsg(userCredentials.v$.password.minLength),
        ]"
        :append-inner-icon="visible ? 'mdi-eye' : 'mdi-eye-off'"
        :type="visible ? 'text' : 'password'"
        placeholder="Wpisz hasło"
        prepend-inner-icon="mdi-lock-outline"
        @click:append-inner="visible = !visible"
      />

      <v-btn
        :disabled="globalStore.loading"
        class="mb-8"
        color="blue"
        size="large"
        variant="tonal"
        block
        @click="handleLogin"
      >
        Zaloguj się
      </v-btn>

      <v-card-text class="text-center">
        <a
          class="text-blue text-decoration-none"
          style="cursor: pointer"
          rel="noopener noreferrer"
          target="_blank"
          @click="goToRegister"
        >
          Zarejestruj się
          <v-icon icon="mdi-chevron-right" />
        </a>
      </v-card-text>
    </v-card>
  </Form>
</template>
<script lang="ts" setup>
import { ref } from 'vue';
import Form from '@/components/Form.vue';
import { useAccountStore } from '@/stores/account.store';
import { AccountRoutesTitles } from '@/data/AccountRoutesTitles';
import { UserCredentials } from '@/models/UserCredentials';
import router from '@/router/router';
import TextField from '@/components/TextField.vue';
import { useGlobalStore } from '@/stores/global.store';

const globalStore = useGlobalStore();
const accountStore = useAccountStore();

const loginForm = ref<InstanceType<typeof Form>>();

const visible = ref(false);

const userCredentials = ref(new UserCredentials());

async function handleLogin(): Promise<void> {
  const loginFormValid = await loginForm.value?.validate();
  if (loginFormValid) {
    await accountStore.login(userCredentials.value);
  }
}

function goToRegister(): void {
  router.push({ name: AccountRoutesTitles.Register });
}
</script>
<style lang="scss" scoped>
.login-form {
  display: flex;
  flex-direction: column;
  width: min(450px, 100%);
  .v-card {
    width: 100%;
  }
}
</style>
