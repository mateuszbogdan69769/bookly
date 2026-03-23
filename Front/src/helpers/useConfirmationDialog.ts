import { IGlobalDialogData } from '@/interfaces/IGlobalDialogData';
import { ref, Ref } from 'vue';

const DEFAULT_DIALOG_DATA: IGlobalDialogData = {
  visible: false,
  title: '',
  message: '',
  confirmBtnText: 'Tak',
  cancelBtnText: 'Nie'
};

const dialogData = ref<IGlobalDialogData>(DEFAULT_DIALOG_DATA);

// eslint-disable-next-line @typescript-eslint/no-unused-vars
const resolveDialog = ref((value: boolean): void => {
  console.error('resolveDialog is not set');
});

export function getConfirmationDialogData(): {
  dialogData: Ref<IGlobalDialogData>;
  resolveDialog: Ref<(value: boolean) => void>;
} {
  return {
    dialogData,
    resolveDialog
  };
}

export function useConfirmationDialog(data: {
  title: string;
  message?: string;
  confirmBtnText?: string;
  cancelBtnText?: string;
}): Promise<boolean> {
  dialogData.value.visible = true;

  dialogData.value.title = data.title;
  dialogData.value.message = data.message ?? DEFAULT_DIALOG_DATA.message;

  dialogData.value.confirmBtnText =
    data.confirmBtnText ?? DEFAULT_DIALOG_DATA.confirmBtnText;
  dialogData.value.cancelBtnText =
    data.cancelBtnText ?? DEFAULT_DIALOG_DATA.cancelBtnText;

  return new Promise((resolve) => {
    resolveDialog.value = resolve;
  });
}
