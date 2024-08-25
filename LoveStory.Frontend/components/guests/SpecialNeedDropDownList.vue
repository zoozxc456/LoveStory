<template>
  <CommonDropDownList v-model:display-controller="displayController">
    <template #presentation>
      <input
        class="w-full h-full px-3 py-3 font-sans text-sm font-normal transition-all bg-transparent border rounded-md peer border-blue-gray-200 text-blue-gray-700 outline outline-0 placeholder-shown:border placeholder-shown:border-blue-gray-200 placeholder-shown:border-t-blue-gray-200 focus:border-2 focus:border-gray-900 focus:border-t-transparent focus:outline-0 disabled:border-0 disabled:bg-blue-gray-50 cursor-pointer"
        placeholder=" "
        readonly
        @click.prevent="displayController.onToggle"
        :value="model?.join(',')"
      />
      <label
        class="before:content[' '] after:content[' '] pointer-events-none absolute left-0 -top-1.5 flex h-full w-full select-none !overflow-visible truncate text-[11px] font-normal leading-tight text-gray-500 transition-all before:pointer-events-none before:mt-[6.5px] before:mr-1 before:box-border before:block before:h-1.5 before:w-2.5 before:rounded-tl-md before:border-t before:border-l before:border-blue-gray-200 before:transition-all after:pointer-events-none after:mt-[6.5px] after:ml-1 after:box-border after:block after:h-1.5 after:w-2.5 after:flex-grow after:rounded-tr-md after:border-t after:border-r after:border-blue-gray-200 after:transition-all peer-placeholder-shown:text-sm peer-placeholder-shown:leading-[4.1] peer-placeholder-shown:text-blue-gray-500 peer-placeholder-shown:before:border-transparent peer-placeholder-shown:after:border-transparent peer-focus:text-[11px] peer-focus:leading-tight peer-focus:text-gray-900 peer-focus:before:border-t-2 peer-focus:before:border-l-2 peer-focus:before:!border-gray-900 peer-focus:after:border-t-2 peer-focus:after:border-r-2 peer-focus:after:!border-gray-900 peer-disabled:text-transparent peer-disabled:before:border-transparent peer-disabled:after:border-transparent peer-disabled:peer-placeholder-shown:text-blue-gray-500"
      >
        特殊需求
      </label>
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
            v-for="item in items"
            class="px-4 py-2 text-sm relative"
            @click.prevent="handleSelectItem(item.trim())"
          >
            <div>{{ item }}</div>
            <div
              v-if="model?.includes(item)"
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
const model = defineModel<string[]>("specialNeeds");

const displayController = useDisplayController();
const items: string[] = ["素食", "兒童椅"];

const handleSelectItem = (item: string) => {
  if (model.value !== undefined) {
    const itemIndex = model.value.findIndex((x) => x === item);
    itemIndex >= 0 ? model.value.splice(itemIndex, 1) : model.value.push(item);
  }
};
</script>
