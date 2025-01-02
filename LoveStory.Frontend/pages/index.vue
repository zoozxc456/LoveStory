<template>
  <div class="flex flex-1 flex-col h-full overflow-auto relative">
    <!-- Search Bar -->
    <RecipientGuestSearchBar
      class="fixed w-[85%] mx-auto bg-white z-10 px-3"
      :titles="searchBarTitles"
    />

    <!-- Guest Card List-->

    <div class="w-full px-3 absolute top-[15%]">
      <RecipientGuestOverviewCard :guests="store.recipientGuests()" />
    </div>
  </div>
</template>

<style scoped lang="scss"></style>

<script setup lang="ts">
import { useRecipientStore } from "stores/recipient/useRecipient";

definePageMeta({ layout: "recipient-layout" });

const searchBarTitles = reactive<string[]>(["男/女方", "賓客關係", "姓名"]);
const store = useRecipientStore();
store.fetchRecipientGuests();
</script>
