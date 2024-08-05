import type { FamilyGuestFormDataType } from "types/GuestManagement/guestFormData.type";

export interface IGuest {
  guestId: string;
  guestName: string;
  guestRelationship: string;
  guestGroup: IGuestGroup | null;
  seatLocation: IBanquetTable;
  isAttended: boolean;
  remark: string;
  createAt: Date;
  creator: IUser;
  specialNeeds: IGuestSpecialNeed[];
}

export interface IGuestSpecialNeed {
  specialNeedId: string;
  specialNeedContent: string;
  guestId: string;
  createAt: Date;
  creator: IUser;
}

export interface IGuestAttendance {
  attendanceId: string;
  arrivalAt: Date;
  guest: IGuest | null;
}

export interface IGuestGroup {
  guestGroupId: string;
  guestGroupName: string;
  remark: string;
  createAt: Date;
  creator: IUser;
}

export type GetGuestManagementResponse = IGuest[];
export type AddGuestManagementRequest = Pick<IGuest, 'guestName' | 'guestRelationship' | 'isAttended' | 'remark'>;
export type AddFamilyGuestRequest = FamilyGuestFormDataType;