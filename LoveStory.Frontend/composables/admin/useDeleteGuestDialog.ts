import { reactive } from 'vue';
import type { IGuest } from '../../types/apis/guest.type';
import { deleteGuest } from '../../apis/guest.api';

export const useDeleteGuestDialog = (emits: (event: 'update:guests') => void) => {
  const data = reactive<Pick<IGuest, 'guestId' | 'guestName'>>({
    guestId: '',
    guestName: ''
  });

  const displayController: { isShow: boolean; onShow: () => boolean; onClose: () => boolean; } = reactive<{ isShow: boolean; onShow: () => boolean; onClose: () => boolean; }>({
    isShow: false,
    onShow: () => displayController.isShow = true,
    onClose: () => displayController.isShow = false
  });

  const handleDeleteGuestById = async (guestId: Pick<IGuest, 'guestId'>) => {
    await deleteGuest(guestId);
    emits('update:guests');
    displayController.onClose();
  };

  const handleCancel = () => {
    cleanData();
    displayController.onClose();
  };

  const handleTriggerShowDialog = ({ guestId, guestName }: Pick<IGuest, 'guestId' | 'guestName'>) => {
    data.guestId = guestId;
    data.guestName = guestName;
    displayController.onShow();
  };

  const cleanData = () => {
    data.guestId = "";
    data.guestName = "";
  };
  return {
    data, isShow: computed(() => displayController.isShow),
    handleCancel, handleDeleteGuestById, handleTriggerShowDialog
  };
};