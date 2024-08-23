<template>
  <div class="w-full h-full mx-auto">
    <div class="header h-[10%] flex justify-center items-center text-2xl">
      賓客管理
    </div>
    <div class="content h-[89%] max-h-[89%] mx-4">
      <GuestsGuestManagementTable
        :guest-managements="guests"
        @update:guests="refreshGuests"
      />
      <GuestsMobileGuestManagementTable
        :guest-managements="guests"
        @update:guests="refreshGuests"
      />
    </div>
  </div>
</template>

<style scoped lang="scss"></style>

<script setup lang="ts">
definePageMeta({ layout: "admin-layout" });
const { guests, isLoading, refreshGuests } = useGuestManagement();
</script>
