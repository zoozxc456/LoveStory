<template>
  <div class="relative h-11 w-full min-w-[200px] mt-3">
    <div ref="dropDownListElementRef">
      <input
        class="w-full h-full px-3 py-3 font-sans text-sm font-normal transition-all bg-transparent border rounded-md peer border-blue-gray-200 border-t-transparent text-blue-gray-700 outline outline-0 placeholder-shown:border placeholder-shown:border-blue-gray-200 placeholder-shown:border-t-blue-gray-200 focus:border-2 focus:border-gray-900 focus:border-t-transparent focus:outline-0 disabled:border-0 disabled:bg-blue-gray-50"
        placeholder=" "
        readonly
        @click="displayController.onToggle"
        :value="relationshipModelValue"
      />
      <label
        class="before:content[' '] after:content[' '] pointer-events-none absolute left-0 -top-1.5 flex h-full w-full select-none !overflow-visible truncate text-[11px] font-normal leading-tight text-gray-500 transition-all before:pointer-events-none before:mt-[6.5px] before:mr-1 before:box-border before:block before:h-1.5 before:w-2.5 before:rounded-tl-md before:border-t before:border-l before:border-blue-gray-200 before:transition-all after:pointer-events-none after:mt-[6.5px] after:ml-1 after:box-border after:block after:h-1.5 after:w-2.5 after:flex-grow after:rounded-tr-md after:border-t after:border-r after:border-blue-gray-200 after:transition-all peer-placeholder-shown:text-sm peer-placeholder-shown:leading-[4.1] peer-placeholder-shown:text-blue-gray-500 peer-placeholder-shown:before:border-transparent peer-placeholder-shown:after:border-transparent peer-focus:text-[11px] peer-focus:leading-tight peer-focus:text-gray-900 peer-focus:before:border-t-2 peer-focus:before:border-l-2 peer-focus:before:!border-gray-900 peer-focus:after:border-t-2 peer-focus:after:border-r-2 peer-focus:after:!border-gray-900 peer-disabled:text-transparent peer-disabled:before:border-transparent peer-disabled:after:border-transparent peer-disabled:peer-placeholder-shown:text-blue-gray-500"
      >
        賓客關係
      </label>
      <div
        class="absolute right-0 z-10 mt-2 w-56 origin-top-right rounded-md bg-white shadow-lg ring-1 ring-black ring-opacity-5 focus:outline-none"
        :class="[displayController.isShow ? 'block' : 'hidden']"
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
    </div>
  </div>
</template>

<style scoped lang="scss"></style>

<script setup lang="ts">
const relations = [
  { label: "男方親人", values: ["男方家人", "新郎父親親戚", "新郎母親親戚"] },
  { label: "女方親人", values: ["女方家人", "新娘父親親戚", "新娘母親親戚"] },
];

const relationshipModelValue = defineModel<string>("relationship");
const dropDownListElementRef = ref<HTMLDivElement | null>(null);

const displayController = reactive({
  isShow: false,
  onShow: () => (displayController.isShow = true),
  onClose: () => (displayController.isShow = false),
  onToggle: () => (displayController.isShow = !displayController.isShow),
});

const handleSelectItem = (relationship: string) => {
  relationshipModelValue.value = relationship;
  displayController.onClose();
};

const handleClickOutside = ({ target }: MouseEvent) => {
  if (
    dropDownListElementRef.value &&
    !dropDownListElementRef.value.contains(target as HTMLElement)
  ) {
    displayController.onClose();
  }
};

onMounted(() => {
  document.addEventListener("click", handleClickOutside);
});

onBeforeUnmount(() => {
  document.removeEventListener("click", handleClickOutside);
});
</script>
