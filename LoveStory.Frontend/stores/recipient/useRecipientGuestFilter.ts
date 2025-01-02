import { defineStore } from 'pinia';

export const useRecipientGuestFilterStore = defineStore('useRecipientGuestFilterStore', () => {
  type RelationshipKeys = "全部" | "男方" | "女方" | "共同朋友";

  const relationships: { [key in RelationshipKeys]: string[] } = {
    全部: ["全部"],
    男方: ["男方家人", "新郎母親親戚", "新郎父親親戚"],
    女方: ["女方家人", "新娘母親親戚", "新娘父親親戚"],
    共同朋友: ["共同朋友"],
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
    
    filterFields,
    maleOrFemaleSelectList,
    relationshipSelectList,
    selectMaleOrFemale,
    selectRelationship,
    enterGuestNameSearchText
  };
});
