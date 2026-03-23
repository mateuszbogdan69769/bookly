<template>
  <v-progress-circular indeterminate :size="small ? 25 : 35" :width="4" />
</template>

<script lang="ts" setup>
defineProps({
  small: { type: Boolean, default: false },
});
</script>

<style lang="scss" scoped></style>
