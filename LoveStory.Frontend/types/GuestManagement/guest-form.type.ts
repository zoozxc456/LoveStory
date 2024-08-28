export interface IBasicGuestOperate<T> {
  state: T;
  handleCancel: () => void;
  clean: () => void;
}

export interface ICreateGuest<T> extends IBasicGuestOperate<T> {
  handleCreate: () => Promise<void>;
}

export interface IModifyGuest<T,K> extends IBasicGuestOperate<T> {
  handleModify: () => Promise<void>;
  convert: (data: K) => void;
}

export interface IDeleteGuest<T> extends IBasicGuestOperate<T> {
  handleDelete: () => Promise<void>;
}