<template>
  <div class="booking-main">
    <NavigationDrawer />

    <div class="booking-main__content">
      <router-view name="viewHeader" />

      <router-view />
    </div>
  </div>
</template>
<script lang="ts" setup>
import NavigationDrawer from '../components/NavigationDrawer.vue';
</script>
<style lang="scss" scoped>
.booking-main {
  height: 100%;
  display: flex;
  &__content {
    overflow: hidden;
    display: flex;
    flex-direction: column;
    width: 100%;
    padding: 20px 16px;
  }
}
</style>
