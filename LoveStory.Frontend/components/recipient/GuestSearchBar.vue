<template>
  <div id="search-bar" class="py-3 flex gap-1">
    <div class="flex-1">
      <CommonDropDownList v-model:display-controller="displayController">
        <template #presentation>
          <input
            class="w-full h-full p-3 font-sans text-sm font-normal transition-all bg-transparent border rounded-md peer border-blue-gray-200 text-blue-gray-700 outline outline-0 placeholder-shown:border placeholder-shown:border-blue-gray-200 placeholder-shown:border-t-blue-gray-200 focus:border-2 focus:border-gray-900 focus:outline-0 disabled:border-0 disabled:bg-blue-gray-50"
            placeholder=" "
            v-model="searchBox.maleOrFemale"
            readonly
            @click="displayController.onShow"
          />
        </template>
        <template #list>
          <div
            class="absolute right-0 z-10 mt-2 w-56 origin-top-right rounded-md bg-white shadow-lg ring-1 ring-black ring-opacity-5 focus:outline-none"
            :class="[displayController.state.isShow ? 'block' : 'hidden']"
            role="menu"
            aria-orientation="vertical"
            aria-labelledby="menu-button"
            tabindex="-1"
          >
            <div class="py-1" role="none">
              <div
                v-for="label in maleOrFemaleList"
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
    </div>

    <div class="flex-1">
      <CommonDropDownList v-model:display-controller="displayController">
        <template #presentation>
          <input
            class="w-full h-full p-3 font-sans text-sm font-normal transition-all bg-transparent border rounded-md peer border-blue-gray-200 text-blue-gray-700 outline outline-0 placeholder-shown:border placeholder-shown:border-blue-gray-200 placeholder-shown:border-t-blue-gray-200 focus:border-2 focus:border-gray-900 focus:outline-0 disabled:border-0 disabled:bg-blue-gray-50"
            placeholder=" "
            v-model="searchBox.maleOrFemale"
            readonly
            @click="displayController.onShow"
          />
        </template>
        <template #list>
          <div
            class="absolute right-0 z-10 mt-2 w-56 origin-top-right rounded-md bg-white shadow-lg ring-1 ring-black ring-opacity-5 focus:outline-none"
            :class="[displayController.state.isShow ? 'block' : 'hidden']"
            role="menu"
            aria-orientation="vertical"
            aria-labelledby="menu-button"
            tabindex="-1"
          >
            <div class="py-1" role="none">
              <div
                v-for="label in maleOrFemaleList"
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
    </div>
  </div>
</template>

<style scoped lang="scss"></style>

<script setup lang="ts">
// type GuestSearchBarProps = {
//   titles: string[];
// };

// const props = defineProps<GuestSearchBarProps>();

const displayController = useDisplayController();
// const displayControllers: { [key in string]: IDisplayController } =
//   props.titles.reduce((acc, title) => {
//     acc[title] = useDisplayController();
//     return acc;
//   }, {} as { [key in string]: IDisplayController });

const maleOrFemaleList = ["男方", "女方", "共同"];

const searchBox = reactive<{ maleOrFemale: string }>({ maleOrFemale: "" });

const handleSelectMaleOrFemale = (maleOrFemale: string) => {
  searchBox.maleOrFemale = maleOrFemale;
  displayController.onClose();
};
</script>
