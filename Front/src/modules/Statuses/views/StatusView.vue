<template>
  <div class="status-view">
    <div class="status-view__header ga-4 pt-2">
      <v-btn color="blue" variant="tonal" @click="addStatus">Dodaj status</v-btn>

      <v-slide-x-reverse-transition>
        <div
          v-if="orderChanged"
          class="d-flex flex-wrap ga-4 pb-1"
          style="overflow: hidden"
        >
          <v-btn color="green" variant="tonal" @click="saveOrder">Zapisz kolejność</v-btn>

          <v-btn color="red" variant="outlined" @click="revertOrder">
            Anuluj kolejność
          </v-btn>
        </div>
      </v-slide-x-reverse-transition>
    </div>

    <div class="status-view__content">
      <v-data-table
        :headers="StatusTableHeaders"
        :items="itemsComputed"
        hide-default-footer
        :items-per-page="25"
        no-data-text="Brak danych"
        :loading="globalStore.loading"
      >
        <template #default="{ items }">
          <thead>
            <tr>
              <th class="v-data-table__td v-data-table__th">
                <div class="v-data-table-header__content">Nazwa</div>
              </th>

              <th class="v-data-table__td v-data-table__th">
                <div class="v-data-table-header__content">Kolor</div>
              </th>

              <th
                class="v-data-table__td v-data-table__th v-data-table-column--align-center"
              >
                <div class="v-data-table-header__content">Akcje</div>
              </th>
            </tr>
          </thead>

          <VueDraggable
            v-model="itemsComputed"
            :animation="150"
            tag="tbody"
            handle=".drag-handle"
            @change="orderChanged = true"
          >
            <tr
              v-for="item in itemsComputed"
              :key="item.id"
              class="v-data-table__tr"
              style="cursor: grab"
            >
              <td class="v-data-table__td v-data-table-column--align-start drag-handle">
                {{ item.name }}
              </td>

              <td class="v-data-table__td v-data-table-column--align-start drag-handle">
                <div :style="`background: ${item.color}`" class="color-box"></div>
              </td>

              <td class="v-data-table__td v-data-table-column--align-start">
                <div class="d-flex justify-center">
                  <v-icon
                    color="medium-emphasis"
                    icon="mdi-pencil"
                    size="small"
                    class="mr-2"
                    @click="editStatus(item.id)"
                  />
                  <v-icon
                    color="medium-emphasis"
                    icon="mdi-delete"
                    size="small"
                    @click="openDeleteDialog(item.id)"
                  />
                </div>
              </td>
            </tr>
          </VueDraggable>
        </template>
      </v-data-table>
    </div>
  </div>

  <v-dialog v-model="dialogVisible" max-width="500">
    <v-card
      :subtitle="isEditing ? 'Edycja statusu' : 'Dodaj status'"
      :title="isEditing ? 'Edytuj' : 'Dodaj'"
    >
      <template #text>
        <Form ref="statusForm" class="d-flex flex-column ga-3">
          <TextField
            v-model="statusData.name"
            :rules="[$validMsg(statusData.v$.name.required)]"
            label="Nazwa"
            placeholder="W trakcie"
          />

          <v-color-picker
            v-model="statusData.color"
            dot-size="15"
            mode="hex"
            style="width: 100%"
          />
        </Form>
      </template>

      <v-divider></v-divider>

      <v-card-actions class="bg-surface-light">
        <v-btn text="Anuluj" variant="plain" @click="dialogVisible = false" />

        <v-spacer />

        <v-btn :text="isEditing ? 'Edytuj' : 'Zapisz'" @click="handleSaveClick" />
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>
<script lang="ts" setup>
import { computed, onMounted, ref } from 'vue';
import { useStatusStore } from '../status.store';
import { useConfirmationDialog } from '@/helpers/useConfirmationDialog';
import Form from '@/components/Form.vue';
import TextField from '@/components/TextField.vue';
import { StatusViewModel } from '../models/StatusViewModel';
import { StatusTableHeaders } from '../data/StatusTableHeaders';
import { VueDraggable } from 'vue-draggable-plus';
import { useGlobalStore } from '@/stores/global.store';
import { Status } from '@/ApiModels/Status';
import { GlobalHelper } from '@/helpers/GlobalHelper';

const globalStore = useGlobalStore();
const statusStore = useStatusStore();

const isEditing = ref(false);
const dialogVisible = ref(false);
const orderChanged = ref(false);

const statusForm = ref<InstanceType<typeof Form>>();

const statusData = ref(new StatusViewModel());
const originalOrderStatuses = ref<Status[]>([]);

const itemsComputed = computed({
  get: () => statusStore.orderedStatuses,
  set: (value) => value.forEach((e, idx) => (e.order = idx)),
});

function addStatus(): void {
  isEditing.value = false;
  statusData.value = new StatusViewModel();
  dialogVisible.value = true;
}

function editStatus(id: number): void {
  const status = statusStore.statuses.find((g) => g.id === id);
  if (!status) return;

  isEditing.value = true;
  statusData.value.setFromStatus(status);
  dialogVisible.value = true;
}

async function openDeleteDialog(id: number): Promise<void> {
  const status = statusStore.statuses.find((g) => g.id === id);
  if (!status) return;

  const title = 'Usuwanie statusu';
  const message = `Czy na pewno chcesz usunąć status "${status.name}"?`;
  const confirmed = await useConfirmationDialog({ title, message });
  if (!confirmed) return;

  await statusStore.deleteStatus(id);
}

async function handleSaveClick(): Promise<void> {
  const formValid = await statusForm.value.validate();
  if (!formValid) return;

  if (isEditing.value) {
    await statusStore.editStatus(statusData.value);
  } else {
    await statusStore.addStatus(statusData.value);
  }

  dialogVisible.value = false;
}

async function saveOrder(): Promise<void> {
  await statusStore.updateStatusOrder();
  orderChanged.value = false;
  originalOrderStatuses.value = GlobalHelper.deepCopy(statusStore.statuses);
}

function revertOrder(): void {
  statusStore.statuses = GlobalHelper.deepCopy(originalOrderStatuses.value);
  orderChanged.value = false;
}

onMounted(async () => {
  await statusStore.loadStatuses();
  originalOrderStatuses.value = GlobalHelper.deepCopy(statusStore.statuses);
});
</script>
<style lang="scss" scoped>
.status-view {
  display: flex;
  flex-direction: column;
  gap: 25px;
  flex-grow: 1;
  overflow: hidden;
  &__header {
    overflow: hidden;
    display: flex;
    align-items: flex-start;
    justify-content: space-between;
  }
  &__content {
    flex-grow: 1;
    overflow: hidden;
    :deep .v-data-table {
      flex-grow: 1;
      height: 100%;
      &__td {
        .color-box {
          width: 40px;
          height: 25px;
          border-radius: 12px;
        }
        span {
          white-space: nowrap;
        }
      }
    }
  }
}
</style>
