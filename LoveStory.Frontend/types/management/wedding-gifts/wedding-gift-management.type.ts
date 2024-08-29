export interface IWeddingGiftManagement {
  managementId: string;
  managementName: string;
  managementType: "single" | "group";
  attendance: WeddingGiftManagementAttendance[];
}

export type WeddingGiftManagementAttendance = Pick<IGuest, 'guestId' | 'guestName' | 'guestRelationship'> & {
  weddingGift?: IWeddingGift;
};

export interface IWeddingGift {
  id: string;
  type: string;
  amount: number;
  receivedAt: Date;
  recepient: IUser;
  remark?: string;
}