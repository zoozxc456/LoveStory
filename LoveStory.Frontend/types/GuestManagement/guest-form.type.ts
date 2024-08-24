export interface IBasicGuestOperate<T> {
  state: T;
  handleCancel: () => void;
  clean: () => void;
}

export interface ICreateGuest<T> extends IBasicGuestOperate<T> {
  handleCreate: () => Promise<void>;
}

export interface IModifyGuest<T> extends IBasicGuestOperate<T> {
  handleModify: () => Promise<void>;
}

export interface IDeleteGuest<T> extends IBasicGuestOperate<T> {
  handleDelete: () => Promise<void>;
}