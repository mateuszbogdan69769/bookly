<template>
  <Form ref="registerForm" class="register-form" @submit="handleRegister">
    <v-card class="mx-auto pa-12 pb-8" elevation="8" max-width="448" rounded="lg">
      <div class="text-subtitle-1 text-medium-emphasis">Imię</div>

      <TextField
        v-model="userCredentials.name"
        :rules="[$validMsg(userCredentials.v$.name.required)]"
        placeholder="Wpisz imię"
        prepend-inner-icon="mdi-account-outline"
      />

      <div class="text-subtitle-1 text-medium-emphasis">E-mail</div>

      <TextField
        v-model="userCredentials.email"
        :rules="[
          $validMsg(userCredentials.v$.email.required),
          $validMsg(userCredentials.v$.email.valid),
          $validMsg(userCredentials.v$.email.taken),
        ]"
        placeholder="Wpisz e-mail"
        prepend-inner-icon="mdi-email-outline"
        @input="userCredentials.emailTaken = false"
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
        @click="handleRegister"
      >
        Zarejestruj się
      </v-btn>

      <v-card-text class="text-center">
        <a
          class="text-blue text-decoration-none"
          style="cursor: pointer"
          rel="noopener noreferrer"
          target="_blank"
          @click="goToLogin"
        >
          Zaloguj się
          <v-icon icon="mdi-chevron-right" />
        </a>
      </v-card-text>
    </v-card>
  </Form>
</template>
<script lang="ts" setup>
import TextField from '../../../components/TextField.vue';
import { ref } from 'vue';
import Form from '@/components/Form.vue';
import { useAccountStore } from '@/stores/account.store';
import { AccountRoutesTitles } from '@/data/AccountRoutesTitles';
import { UserCredentials } from '@/models/UserCredentials';
import router from '@/router/router';
import { useGlobalStore } from '@/stores/global.store';

const globalStore = useGlobalStore();
const accountStore = useAccountStore();

const registerForm = ref<InstanceType<typeof Form>>();

const visible = ref(false);

const userCredentials = ref(new UserCredentials());

async function handleRegister(): Promise<void> {
  await userCredentials.value.checkEmailAvailability();
  const registerFormValid = await registerForm.value?.validate();
  if (registerFormValid) {
    await accountStore.register(userCredentials.value);
  }
}

function goToLogin(): void {
  router.push({ name: AccountRoutesTitles.Login });
}
</script>
<style lang="scss" scoped>
.register-form {
  display: flex;
  flex-direction: column;
  width: min(450px, 100%);
  .v-card {
    width: 100%;
  }
}
</style>
