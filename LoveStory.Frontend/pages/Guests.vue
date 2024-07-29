<template>
  <div class="w-full mx-auto">
    <div class="header text-center text-2xl">賓客管理</div>
    <div class="h-12"></div>
    <GuestManagementTable
      :guest-managements="guests"
      @update:guests="refreshGuests"
    />
  </div>
</template>

<style scoped lang="scss"></style>

<script setup lang="ts">
import GuestManagementTable from "../components/guests/GuestManagementTable.vue";
import { useGuestManagement } from "../composables/admin/useGuestManagement";
const { guests, isLoading, refreshGuests } = useGuestManagement();
</script>
