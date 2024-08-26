export type SingleGuestFormDataType = Pick<IGuest, 'guestName' | 'guestRelationship' | 'isAttended' | 'remark'> & { specialNeeds: string[]; seatLocation: Pick<IBanquetTable, 'banquetTableId'> | null; };
export type FamilyGuestFormDataType = {
  familyName: string;
  guestRelationship: string;
  isAttended: boolean;
  attendance: FamilyAttendanceDataType[];
  seatLocation: Pick<IBanquetTable, 'banquetTableId'> | null;
};

export type FamilyAttendanceDataType = Pick<IGuest, "guestName" | "remark"> & { specialNeeds: string[]; };

export type GuestType = 'single' | 'family';

// Modify Form 

export type ModifySingleGuestFormDataType = Omit<IGuest, 'seatLocation'> & { seatLocation: Pick<IBanquetTable, 'banquetTableId'> | null; };
export type ModifyFamilyGuestFormDataType = Pick<IGuestGroup, "guestGroupId" | "guestGroupName"> & Pick<IGuest, "guestRelationship" | "isAttended"> & { attendance: ModifySingleGuestFormDataType[]; seatLocation: Pick<IBanquetTable, 'banquetTableId'> | null; };

export type DeleteGuestFormDataType = Pick<IGuest, 'guestId' | 'guestName'>;