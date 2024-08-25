export const useFamilyGuestsFormDataValidator = (): IFormDataValidator<FamilyGuestFormDataType> => {

  const valid = ({ familyName, guestRelationship, isAttended, attendance, seatLocation }: FamilyGuestFormDataType): boolean => {
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

export const useModifyFamilyGuestFormDataValidator =():IFormDataValidator<ModifyFamilyGuestFormDataType>=>{
  const valid = ({ guestGroupName, guestRelationship, isAttended, attendance, seatLocation }: ModifyFamilyGuestFormDataType): boolean => {
    if (guestGroupName === "") return false;
    if (guestRelationship === "") return false;
    if (attendance.some(x => x.guestName === "")) return false;
    if (isAttended && seatLocation === null) return false;
    if (!isAttended && seatLocation !== null) return false;

    return true;
  };

  return ({ valid });
}

export const useModifySingleGuestFormDataValidator =():IFormDataValidator<ModifySingleGuestFormDataType>=>{
  const valid = ({ guestName, guestRelationship, isAttended, seatLocation }: ModifySingleGuestFormDataType): boolean => {
    if (guestName === "") return false;
    if (guestRelationship === "") return false;
    if (isAttended && seatLocation === null) return false;
    if (!isAttended && seatLocation !== null) return false;

    return true;
  };

  return ({ valid });
}