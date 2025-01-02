<template>
  <div id="search-bar" class="py-3">
    <div class="flex w-full p-3 items-center gap-1">
      <div class="flex-1">
        <h6 class="text-2xl font-bold text-gray-700">賓客名稱</h6>
      </div>
      <div class="flex-[8]">
        <input
          class="w-full h-full px-3 py-3 font-sans text-sm font-normal transition-all bg-transparent border rounded-md peer border-blue-gray-200 text-blue-gray-700 outline outline-0 placeholder-shown:border placeholder-shown:border-blue-gray-200 placeholder-shown:border-t-blue-gray-200 focus:border-2 focus:border-gray-900 focus:outline-0 disabled:border-0 disabled:bg-blue-gray-50"
          placeholder=" "
          v-model="store.filterFields.guestNameSearchText"
        />
      </div>
    </div>

    <div class="flex w-full p-3 items-center gap-1">
      <div class="flex-1">
        <h6 class="text-2xl font-bold text-gray-700">賓客分類</h6>
      </div>
      <div class="flex-[8] grid grid-cols-2 gap-1">
        <CommonDropDownList
          v-model:display-controller="displayControllers['maleOrFemale']"
        >
          <template #presentation>
            <input
              class="w-full h-full p-3 font-sans text-sm font-normal transition-all bg-transparent border rounded-md peer border-blue-gray-200 text-blue-gray-700 outline outline-0 placeholder-shown:border placeholder-shown:border-blue-gray-200 placeholder-shown:border-t-blue-gray-200 focus:border-2 focus:border-gray-900 focus:outline-0 disabled:border-0 disabled:bg-blue-gray-50"
              placeholder=" "
              v-model="store.filterFields.maleOrFemale"
              readonly
              @click="displayControllers['maleOrFemale'].onShow"
            />
          </template>
          <template #list>
            <div
              class="absolute right-0 z-10 mt-2 w-56 origin-top-right rounded-md bg-white shadow-lg ring-1 ring-black ring-opacity-5 focus:outline-none"
              :class="[
                displayControllers['maleOrFemale'].state.isShow
                  ? 'block'
                  : 'hidden',
              ]"
              role="menu"
              aria-orientation="vertical"
              aria-labelledby="menu-button"
              tabindex="-1"
            >
              <div class="py-1" role="none">
                <div
                  v-for="label in maleOrFemaleSelectList()"
                  :key="label"
                  @click="handleSelectMaleOrFemale(label)"
                  class="text-gray-400 text-sm p-3 hover:cursor-pointer"
                >
                  <label>{{ label }}</label>
                </div>
              </div>
            </div>
          </template>
        </CommonDropDownList>

        <CommonDropDownList
          v-model:display-controller="displayControllers['relationship']"
        >
          <template #presentation>
            <input
              class="w-full h-full p-3 font-sans text-sm font-normal transition-all bg-transparent border rounded-md peer border-blue-gray-200 text-blue-gray-700 outline outline-0 placeholder-shown:border placeholder-shown:border-blue-gray-200 placeholder-shown:border-t-blue-gray-200 focus:border-2 focus:border-gray-900 focus:outline-0 disabled:border-0 disabled:bg-blue-gray-50"
              placeholder=" "
              v-model="store.filterFields.relationship"
              readonly
              @click="displayControllers['relationship'].onShow"
            />
          </template>
          <template #list>
            <div
              class="absolute right-0 z-10 mt-2 w-56 origin-top-right rounded-md bg-white shadow-lg ring-1 ring-black ring-opacity-5 focus:outline-none"
              :class="[
                displayControllers['relationship'].state.isShow
                  ? 'block'
                  : 'hidden',
              ]"
              role="menu"
              aria-orientation="vertical"
              aria-labelledby="menu-button"
              tabindex="-1"
            >
              <div class="py-1" role="none">
                <div
                  v-for="label in relationshipSelectList()"
                  :key="label"
                  @click="handleSelectRelationship(label)"
                  class="text-gray-400 text-sm p-3 hover:cursor-pointer"
                >
                  <label>{{ label }}</label>
                </div>
              </div>
            </div>
          </template>
        </CommonDropDownList>
      </div>
    </div>
  </div>
</template>

<style scoped lang="scss"></style>

<script setup lang="ts">
import { useRecipientGuestFilterStore } from "stores/recipient/useRecipientGuestFilter";

const displayControllers: {
  [key in "maleOrFemale" | "relationship"]: IDisplayController;
} = {
  maleOrFemale: useDisplayController(),
  relationship: useDisplayController(),
};

type RelationshipKeys = "全部" | "男方" | "女方" | "共同朋友";

const store = useRecipientGuestFilterStore();

const {
  maleOrFemaleSelectList,
  relationshipSelectList,
  selectMaleOrFemale,
  selectRelationship,
} = store;

const handleSelectMaleOrFemale = (maleOrFemale: RelationshipKeys) => {
  selectMaleOrFemale(maleOrFemale);
  displayControllers["maleOrFemale"].onClose();
};

const handleSelectRelationship = (relationship: string) => {
  selectRelationship(relationship);
  displayControllers["relationship"].onClose();
};
</script>
