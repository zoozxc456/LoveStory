const deleteGuestRequest = async (id: Pick<IGuest, 'guestId'>) => $fetch(`/api/admin/guests/${id}`, { method: 'DELETE', headers: { ...generateJwtAuthorizeHeader() } });

export const useDeleteGuestDialog = (displayController: IDialogDisplayController, emits: (event: 'update:guests') => void) => {
  const data = reactive<Pick<IGuest, 'guestId' | 'guestName'>>({
    guestId: '',
    guestName: ''
  });

  const handleDeleteGuestById = async (guestId: Pick<IGuest, 'guestId'>) => {
    await deleteGuestRequest(guestId);
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
    data, isShow: computed(() => displayController.state.isShow),
    handleCancel, handleDeleteGuestById, handleTriggerShowDialog
  };
};