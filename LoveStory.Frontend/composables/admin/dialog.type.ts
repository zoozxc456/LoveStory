import type { IGuest } from "../../types/apis/guest.type";

export type CreateSingleGuestFormDataType = Pick<IGuest, 'guestName' | 'guestRelationship' | 'isAttended' | 'remark'> & { specialNeeds: string[]; };
export type CreateFamilyGuestFormDataType = {
  familyName: string;
  relationship: string;
  isAttended: boolean;
  attendance: FamilyAttendanceDataType[];
};

export type FamilyAttendanceDataType = Pick<IGuest, "guestName" | "remark"> & { specialNeeds: string[]; };

export type GuestType = 'single' | 'family';