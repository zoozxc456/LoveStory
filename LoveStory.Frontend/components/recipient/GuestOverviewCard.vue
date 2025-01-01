<template>
  <div class="w-full grid grid-cols-4 gap-5 max-h-[90dvh] overflow-auto">
    <div
      class="h-[200px] p-3 border rounded text-center text-gray-700 flex flex-col"
      v-for="guest in props.guests"
      :key="guest.targetId"
    >
      <div class="card-header flex items-center justify-center h-1/5 p-4">
        <h6 class="text-2xl font-bold">{{ guest.guestName }}</h6>
      </div>

      <div class="card-body flex justify-center items-center h-1/2 p-4">
        <div class="flex-[3]">{{ guest.relationship }}</div>
        <div class="flex-1">{{ `${guest.attendanceAmount}人` }}</div>
      </div>

      <div class="card-footer flex items-center justify-center h-1/3 p-4">
        <CommonButtonPrimaryButton
          :text="'報到'"
          class="w-[95%]"
          @click="handleGuestArrive(guest.targetId)"
        />
      </div>
    </div>
  </div>
</template>

<style scoped lang="scss"></style>

<script setup lang="ts">
import { useRecipientStore } from "stores/recipient/useRecipient";

type GuestOverviewCardProps = {
  guests: RecipientGuest[];
};

const props = defineProps<GuestOverviewCardProps>();

const handleGuestArrive = (guestId: string) => {
  useRecipientStore().guestArrive(guestId);
};
</script>
