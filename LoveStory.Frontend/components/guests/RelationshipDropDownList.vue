<template>
  <CommonDropDownList v-model:display-controller="displayController">
    <template #presentation>
      <input
        class="w-full h-full p-3 font-sans text-sm font-normal transition-all bg-transparent border rounded-md peer border-blue-gray-200 text-blue-gray-700 outline outline-0 placeholder-shown:border placeholder-shown:border-blue-gray-200 placeholder-shown:border-t-blue-gray-200 focus:border-2 focus:border-gray-900 focus:outline-0 disabled:border-0 disabled:bg-blue-gray-50"
        placeholder=" "
        readonly
        @click="displayController.onToggle"
        :value="relationshipModelValue"
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
          <div v-for="{ label, values } in relations">
            <label class="text-gray-400 text-xs px-3 py-1">{{ label }}</label>
            <div
              v-for="relationship in values"
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
const relations = [
  { label: "男方親人", values: ["男方家人", "新郎父親親戚", "新郎母親親戚"] },
  { label: "女方親人", values: ["女方家人", "新娘父親親戚", "新娘母親親戚"] },
];

const relationshipModelValue = defineModel<string>("relationship");
const displayController: IDisplayController = useDisplayController();

const handleSelectItem = (relationship: string) => {
  relationshipModelValue.value = relationship;
  displayController.onClose();
};
</script>
