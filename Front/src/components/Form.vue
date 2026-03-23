<template>
  <v-form ref="vform" :class="uniqClass" @submit.prevent="handleSubmit">
    <slot />

    <v-btn type="submit" :disabled="!!globalStore.loading" class="d-none" />
  </v-form>
</template>
<script lang="ts" setup>
import { AnimationHelper } from '@/helpers/AnimationHelper';
import { GlobalHelper } from '@/helpers/GlobalHelper';
import { useGlobalStore } from '@/stores/global.store';
import { ref } from 'vue';

const emit = defineEmits<{
  (e: 'submit'): void;
}>();

const globalStore = useGlobalStore();
const vform = ref();

const uniqClass = ref(`v-form-id-${GlobalHelper.getAppUID()}`);

function handleSubmit(): void {
  if (globalStore.loading) return;
  emit('submit');
}

async function validate(): Promise<boolean> {
  const { valid } = await vform.value.validate();
  AnimationHelper.shakeElement(`.${uniqClass.value} .v-input--error .v-messages`);
  return valid;
}

function reset(): void {
  vform.value.reset();
}

function resetValidation(): void {
  vform.value.resetValidation();
}

defineExpose({ validate, reset, resetValidation });
</script>
