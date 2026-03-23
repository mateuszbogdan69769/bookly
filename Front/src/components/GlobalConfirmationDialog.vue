<template>
  <v-dialog v-model="dialogData.visible" max-width="500">
    <v-card rounded="lg">
      <v-card-title class="text-h6 text-center my-2">{{ dialogData.title }}</v-card-title>

      <v-card-text v-if="dialogData.message">
        <p>{{ dialogData.message }}</p>
      </v-card-text>

      <v-divider></v-divider>

      <v-card-actions>
        <v-spacer />

        <v-btn color="secondary" text @click="cancel">
          {{ dialogData.cancelBtnText }}
        </v-btn>

        <v-btn color="primary" text @click="confirm">
          {{ dialogData.confirmBtnText }}
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>
<script lang="ts" setup>
import { getConfirmationDialogData } from '@/helpers/useConfirmationDialog';

const { dialogData, resolveDialog } = getConfirmationDialogData();

function confirm(): void {
  resolveDialog.value(true);
  dialogData.value.visible = false;
}

function cancel(): void {
  resolveDialog.value(false);
  dialogData.value.visible = false;
}
</script>
