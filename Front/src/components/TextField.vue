<template>
  <v-text-field
    v-model="text"
    :label="label"
    :rules="rules"
    :disabled="disabled"
    :hide-details="hideDetails"
    :placeholder="placeholder"
    :prepend-inner-icon="prependIcon"
    :type="type"
    :min-width="110"
    density="compact"
    variant="outlined"
  />
</template>
<script lang="ts" setup>
import { computed, PropType } from 'vue';

const emit = defineEmits<{
  (e: 'update:modelValue', text: string): void;
}>();

const props = defineProps({
  modelValue: {
    type: String,
    required: true,
  },
  label: {
    type: String,
    default: '',
  },
  placeholder: {
    type: String,
    default: '',
  },
  prependIcon: {
    type: String,
    default: '',
  },
  type: {
    type: String,
    default: 'text',
  },
  rules: { type: Array as PropType<Array<true | string>>, default: () => [] },
  disabled: { type: Boolean, default: false },
  hideDetails: { type: Boolean, default: false },
});

const text = computed({
  get: () => props.modelValue,
  set: (v) => emit('update:modelValue', v),
});
</script>
<style lang="scss" scoped></style>
