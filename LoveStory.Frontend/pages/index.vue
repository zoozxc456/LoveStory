<template>
  <div>
    <h1>招待</h1>

    <!-- Search Bar -->
    <RecipientGuestSearchBar :titles="searchBarTitles" />

    <!-- Guest Card List-->

    <div class="w-full px-3 max-h-[90dvh] overflow-auto">
      <RecipientGuestOverviewCard :guests="recipientGuests()" />
    </div>
  </div>
</template>

<style scoped lang="scss"></style>

<script setup lang="ts">
import { useRecipientStore } from "stores/recipient/useRecipient";

definePageMeta({ layout: "recipient-layout" });

const searchBarTitles = reactive<string[]>(["男/女方", "賓客關係", "姓名"]);

const { recipientGuests } = useRecipientStore();
useRecipientStore().fetchRecipientGuests();
</script>
