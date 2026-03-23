<template>
  <div class="register-view">
    <RegisterForm />
  </div>
</template>
<script lang="ts" setup>
import RegisterForm from '../components/RegisterForm.vue';
</script>
<style lang="scss" scoped>
.register-view {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  height: 100%;
}
</style>
