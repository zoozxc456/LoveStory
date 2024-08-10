export interface IDialogDisplayController {
  state: { isShow: boolean; };
  onShow: () => boolean;
  onClose: () => boolean;
  onToggle: () => boolean;
}

export const useDialogDisplayController = (): IDialogDisplayController => {
  const state = reactive<{ isShow: boolean; }>({ isShow: false });

  const onShow = () => state.isShow = true;
  const onClose = () => state.isShow = false;
  const onToggle = () => state.isShow = !state.isShow;

  return {
    state,
    onShow,
    onClose,
    onToggle
  };
};