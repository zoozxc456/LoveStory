export type SingleGuestFormDataType = Pick<IGuest, 'guestName' | 'guestRelationship' | 'isAttended' | 'remark'> & { specialNeeds: string[]; };
export type FamilyGuestFormDataType = {
  familyName: string;
  guestRelationship: string;
  isAttended: boolean;
  attendance: FamilyAttendanceDataType[];
};

export type FamilyAttendanceDataType = Pick<IGuest, "guestName" | "remark"> & { specialNeeds: string[]; };

export type GuestType = 'single' | 'family';


// Modify Form 

export type ModifySingleGuestFormDataType = Omit<IGuest, 'seatLocation'>;
// export type ModifyFamilyGuestFormDataType = (Omit<IGuest, 'seatLocation'>)[];

// export type ModifyFamilyGuestFormDataType = ({
//   groupId: string;
//   familyName: string;
//   guestRelationship: string;
//   isAttended: boolean;
//   attendance: ModifySingleGuestFormDataType[];
// });

export type ModifyFamilyGuestFormDataType = Pick<IGuestGroup, "guestGroupId" | "guestGroupName"> & Pick<IGuest, "guestRelationship" | "isAttended"> & { attendance: ModifySingleGuestFormDataType[]; };