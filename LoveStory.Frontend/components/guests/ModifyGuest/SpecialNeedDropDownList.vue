<template>
  <CommonDropDownList v-model:display-controller="displayController">
    <template #presentation>
      <input
        class="w-full h-full px-3 py-3 font-sans text-sm font-normal transition-all bg-transparent border rounded-md peer border-blue-gray-200 text-blue-gray-700 outline outline-0 placeholder-shown:border placeholder-shown:border-blue-gray-200 placeholder-shown:border-t-blue-gray-200 focus:border-2 focus:border-gray-900 focus:outline-0 disabled:border-0 disabled:bg-blue-gray-50 cursor-pointer"
        placeholder=" "
        readonly
        @click.prevent="displayController.onToggle"
        :value="specialNeeds.map((x) => x.specialNeedContent).join(',')"
      />
    </template>
    <template #list>
      <div
        class="absolute right-0 z-10 mt-2 w-full origin-top-right rounded-md bg-white shadow-lg ring-1 ring-black ring-opacity-5 focus:outline-none"
        :class="[displayController.state.isShow ? 'block' : 'hidden']"
        role="menu"
        aria-orientation="vertical"
        aria-labelledby="menu-button"
        tabindex="-1"
      >
        <div class="py-1" role="none">
          <div
            v-for="(item, index) in items"
            :key="index"
            class="px-4 py-2 text-sm relative"
            @click.prevent="handleSelectItem(item.trim())"
          >
            <div>{{ item }}</div>
            <div
              v-if="
                specialNeeds.map((x) => x.specialNeedContent).includes(item)
              "
              class="w-[16px] h-[16px] absolute top-1/2 right-2 -translate-y-1/2"
            >
              <font-awesome-icon :icon="['fas', 'check']" :size="'2xs'" />
            </div>
          </div>
        </div>
      </div>
    </template>
  </CommonDropDownList>
</template>

<style scoped lang="scss"></style>

<script setup lang="ts">
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
const specialNeeds = defineModel<IGuestSpecialNeed[]>("specialNeeds", {
  required: true,
});

const displayController = useDisplayController();
const items: string[] = ["素食", "兒童椅"];

const handleSelectItem = (item: string) => {
  const itemIndex = specialNeeds.value.findIndex(
    (x) => x.specialNeedContent === item
  );

  if (itemIndex >= 0) {
    specialNeeds.value.splice(itemIndex, 1);
  } else {
    specialNeeds.value.push({
      specialNeedId: "00000000-0000-0000-0000-000000000000",
      specialNeedContent: item,
      guestId: "",
      createAt: new Date(),
      creator: {
        userId: "",
        username: "",
      },
    });
  }
};
</script>
