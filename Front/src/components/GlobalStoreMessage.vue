<template>
  <div class="global-messages-wrapper">
    <v-slide-y-reverse-transition group>
      <v-alert
        v-for="(message, index) in MessageHelper.messages"
        :key="`message-${index}`"
        :type="message.type"
        :title="message.title"
        :text="message.content"
        :class="message.uniqClass"
        density="compact"
        variant="outlined"
        border="top"
      />
    </v-slide-y-reverse-transition>
  </div>
</template>
<script lang="ts" setup>
import { MessageHelper } from '@/helpers/MessageHelper';
</script>
<style lang="scss" scoped>
.global-messages-wrapper {
  position: absolute;
  left: 0;
  bottom: 20px;
  display: flex;
  flex-direction: column;
  align-items: center;
  gap: 10px;
  width: 100%;
  padding: 0 20px;
  .v-alert:deep() {
    width: min(100%, 400px);
    background: #fff;
    .v-alert-title {
      font-size: 14px;
    }
  }
}
</style>
