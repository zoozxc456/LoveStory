<template>
  <div class="w-full mx-auto">
    <div class="header text-center text-2xl">賓客管理</div>
    <div class="h-12"></div>
    <GuestsGuestManagementTable
      :guest-managements="guests"
      @update:guests="refreshGuests"
    />
  </div>
</template>

<style scoped lang="scss"></style>

<script setup lang="ts">
definePageMeta({ layout: "admin-layout" });
const { guests, isLoading, refreshGuests } = useGuestManagement();
</script>
