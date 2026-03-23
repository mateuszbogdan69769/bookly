<template>
  <div class="account-main">
    <router-view class="account-main__router-view" />
  </div>
</template>
<script lang="ts" setup></script>
<style lang="scss" scoped>
.account-main {
  height: 100%;
  display: flex;
  &__router-view {
    width: 100%;
    padding: 20px 16px;
  }
}
</style>
