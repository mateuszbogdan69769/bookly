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
        :model-value="isoTime"
        :label="label"
        readonly
        hide-details
        v-bind="props"
      />
    </template>

    <v-time-picker v-model="isoTime" format="24hr" hide-header />
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
    default: 'Wybierz godzinę',
  },
});

const menuVisible = ref(false);

const isoTime = computed({
  get: () =>
    DateHelper.GetDisplayDateInCurrentTimeZone(props.modelValue, {
      showTime: true,
      showDate: false,
    }),
  set: (v) => {
    const [hour, minute] = v.split(':').map(Number);

    const localTime = DateTime.now().set({ hour, minute });

    const dateTimeUtc = localTime.toUTC();

    const updated = props.modelValue.set({
      hour: dateTimeUtc.hour,
      minute: dateTimeUtc.minute,
    });

    emit('update:modelValue', updated);
  },
});
</script>
<style lang="scss" scoped></style>
