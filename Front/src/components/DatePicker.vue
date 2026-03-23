<template>
  <v-menu
    v-model="menuVisible"
    :close-on-content-click="false"
    transition="scale-transition"
    offset-y
    min-width="auto"
  >
    <template #activator="{ props }">
      <TextField
        :model-value="isoDate.slice(0, 10)"
        :label="label"
        readonly
        hide-details
        v-bind="props"
      />
    </template>

    <v-date-picker v-model="isoDate" @input="menuVisible = false" hide-header />
  </v-menu>
</template>
<script lang="ts" setup>
import { DateHelper } from '@/helpers/DateHelper';
import { DateTime } from 'luxon';
import { computed, PropType, ref } from 'vue';
import TextField from './TextField.vue';

const emit = defineEmits<{
  (e: 'update:modelValue', date: DateTime): void;
}>();

const props = defineProps({
  modelValue: {
    type: Object as PropType<DateTime>,
    required: true,
  },
  label: {
    type: String,
    default: 'Wybierz datę',
  },
});

const menuVisible = ref(false);

const isoDate = computed({
  get: () => DateHelper.GetDateInCurrentTimeZone(props.modelValue).toISO(),
  set: (v) => {
    const isoString = (v as Date).toISOString();
    const dateTimeUtc = DateTime.fromISO(isoString, { zone: 'utc' });
    emit('update:modelValue', dateTimeUtc);
    menuVisible.value = false;
  },
});
</script>
<style lang="scss" scoped></style>
