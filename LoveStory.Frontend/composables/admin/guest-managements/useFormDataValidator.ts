export const useFamilyGuestsFormDataValidator = (): IFormDataValidator<FamilyGuestFormDataType> => {

  const valid = ({ familyName, guestRelationship, isAttended, attendance, seatLocation }: FamilyGuestFormDataType): boolean => {
    console.log(`===== Start Console.log for validator =====`);
    console.log({ familyName, guestRelationship, isAttended, attendance, seatLocation });
    console.log(`===== End Console.log for validator =====`);
    if (familyName === "") return false;
    if (guestRelationship === "") return false;
    if (attendance.some(x => x.guestName === "")) return false;
    if (isAttended && seatLocation === null) return false;
    if (!isAttended && seatLocation !== null) return false;

    return true;
  };

  return ({ valid });
};

export const useSingleGuestFormDataValidator = (): IFormDataValidator<SingleGuestFormDataType> => {
  const valid = ({ guestName, guestRelationship, isAttended, seatLocation }: SingleGuestFormDataType) => {
    if (guestName === "") return false;
    if (guestRelationship === "") return false;
    if (isAttended && seatLocation === null) return false;
    if (!isAttended && seatLocation !== null) return false;

    return true;
  };

  return ({ valid });
};