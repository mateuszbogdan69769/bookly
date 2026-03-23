<template>
  <Spinner class="global-loader" />
</template>

<script lang="ts" setup>
import Spinner from './Spinner.vue';
</script>

<style lang="scss" scoped>
.global-loader {
  position: absolute;
  left: 50%;
  top: 50%;
  transform: translate(-50%, -50%);
}
</style>
