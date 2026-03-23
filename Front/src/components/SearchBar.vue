<template>
  <TextField
    v-model="searchText"
    :placeholder="placeholder"
    prepend-icon="mdi-magnify"
    hide-details
  />
</template>
<script lang="ts" setup>
import TextField from './TextField.vue';
import { computed } from 'vue';

const emit = defineEmits<{
  (e: 'update:modelValue', value: string): void;
}>();

const props = defineProps({
  modelValue: { type: String, required: true },
  placeholder: {
    type: String,
    default: 'Wpisz szukaną frazę...',
  },
  fullWidth: { type: Boolean, default: false },
});

const searchText = computed({
  get: () => props.modelValue,
  set: (value) => emit('update:modelValue', value),
});
</script>
<style lang="scss" scoped></style>
