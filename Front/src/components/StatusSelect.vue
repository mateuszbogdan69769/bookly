<template>
  <v-select
    v-model="status"
    :items="statusStore.orderedStatuses"
    :variant="plain ? 'plain' : 'outlined'"
    :label="plain ? '' : 'Status'"
    hide-details
    density="compact"
    item-title="name"
    return-object
  >
    <template #selection="{ item }">
      <div class="d-flex align-center selected-item">
        <div :style="{ background: item.raw.color }" class="dot mr-2" />

        <span>{{ item.raw.name }} </span>
      </div>
    </template>

    <template #item="{ item, props }">
      <v-list-item v-bind="props">
        <template #prepend>
          <div :style="{ background: item.raw.color }" class="dot mr-2" />
        </template>
      </v-list-item>
    </template>
  </v-select>
</template>
<script lang="ts" setup>
import { useStatusStore } from '@/modules/Statuses/status.store';
import { nextTick } from 'vue';
import { computed, onMounted } from 'vue';

const emit = defineEmits<{
  (e: 'update:modelValue', statusId: number): void;
}>();

const props = defineProps({
  modelValue: {
    type: Number,
    required: true,
  },
  plain: {
    type: Boolean,
    default: false,
  },
});

const statusStore = useStatusStore();

const status = computed({
  get: () => statusStore.orderedStatuses.find((s) => s.id === props.modelValue),
  set: (v) => emit('update:modelValue', v.id),
});

onMounted(async () => {
  if (!statusStore.statuses.length) {
    await statusStore.loadStatuses();
  }

  await nextTick();

  if (!status.value && statusStore.orderedStatuses.length) {
    status.value = statusStore.orderedStatuses[0];
  }
});
</script>
<style lang="scss" scoped>
.dot {
  min-width: 15px;
  width: 15px;
  height: 15px;
  border-radius: 50%;
}
:deep .v-select__selection {
  overflow: hidden;
  .selected-item {
    overflow: hidden;
    span {
      overflow: hidden;
      text-overflow: ellipsis;
      white-space: nowrap;
    }
  }
}
</style>
