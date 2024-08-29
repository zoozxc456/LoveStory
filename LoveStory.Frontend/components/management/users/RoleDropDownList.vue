<template>
  <CommonDropDownList v-model:display-controller="displayController">
    <template #presentation>
      <input
        class="w-full h-full p-3 font-sans text-sm font-normal transition-all bg-transparent border rounded-md peer border-blue-gray-200 text-blue-gray-700 outline outline-0 placeholder-shown:border placeholder-shown:border-blue-gray-200 placeholder-shown:border-t-blue-gray-200 focus:border-2 focus:border-gray-900 focus:outline-0 disabled:border-0 disabled:bg-blue-gray-50"
        placeholder=" "
        readonly
        @click="displayController.onToggle"
        :value="roleModelValue"
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
          <div v-for="{ label, values } in roles" :key="label">
            <label class="text-gray-400 text-xs px-3 py-1">{{ label }}</label>
            <div
              v-for="(relationship, index) in values"
              :key="index"
              class="px-4 py-2 text-sm"
              @click.stop="handleSelectItem(`${label}/${relationship}`)"
            >
              {{ relationship }}
            </div>
          </div>
        </div>
      </div>
    </template>
  </CommonDropDownList>
</template>

<style scoped lang="scss"></style>

<script setup lang="ts">
import { useDisplayController,type IDisplayController } from ".nuxt/imports";

const roles = [
  { label: "管理者", values: ["新郎", "新娘"] },
  { label: "小幫手", values: ["招待", "伴郎", "伴娘"] },
];

const roleModelValue = defineModel<string>("role", { required: true });
const displayController: IDisplayController = useDisplayController();

const handleSelectItem = (role: string) => {
  roleModelValue.value = role;
  displayController.onClose();
};
</script>
