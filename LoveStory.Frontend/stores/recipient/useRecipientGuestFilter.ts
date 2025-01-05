import { defineStore } from 'pinia';

export const useRecipientGuestFilterStore = defineStore('useRecipientGuestFilterStore', () => {
  type RelationshipKeys = "全部" | "主婚人" | "男方" | "女方" | "共同朋友";

  const relationships: { [key in RelationshipKeys]: string[] } = {
    全部: ["全部"],
    主婚人: ["主婚人(含新人)"],
    男方: ["全部", "男方親戚(主辦)", "男方親戚(姑/叔)", "男方親戚(姨/舅)", "男方同事", "男方朋友(大學同學)", "男方不出席賓客"],
    女方: ["全部", "女方親戚(姑/伯)",
      "女方親戚(姨/舅)",
      "女方親戚(水門)", "女方父母朋友", "女方同事", "女方不出席賓客", "女方朋友(國中/大學同學)"],
    共同朋友: ["男女方共同朋友"],
  };

  const filterFields = reactive<{
    maleOrFemale: RelationshipKeys;
    relationship: string;
    guestNameSearchText: string;
  }>({
    maleOrFemale: "全部",
    relationship: "全部",
    guestNameSearchText: ''
  });

  const maleOrFemaleSelectList = (): RelationshipKeys[] =>
    Object.keys(relationships) as RelationshipKeys[];

  const relationshipSelectList = () => {
    if (filterFields.maleOrFemale === "全部")
      return Object.values(relationships).flat();

    return relationships[filterFields.maleOrFemale];
  };

  const selectMaleOrFemale = (
    maleOrFemale: keyof typeof relationships
  ) => {
    filterFields.maleOrFemale = maleOrFemale;
    filterFields.relationship = relationships[maleOrFemale][0];
  };

  const selectRelationship = (relationship: string) => {
    filterFields.relationship = relationship;
  };

  const enterGuestNameSearchText = (event: Event) => {
    filterFields.guestNameSearchText = (event.target as HTMLInputElement).value;
  };

  return {
    relationships,
    filterFields,
    maleOrFemaleSelectList,
    relationshipSelectList,
    selectMaleOrFemale,
    selectRelationship,
    enterGuestNameSearchText
  };
});
