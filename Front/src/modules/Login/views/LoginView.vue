<template>
  <div class="login-view">
    <LoginForm />
  </div>
</template>
<script lang="ts" setup>
import LoginForm from '../components/LoginForm.vue';
</script>
<style lang="scss" scoped>
.login-view {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  height: 100%;
}
</style>
