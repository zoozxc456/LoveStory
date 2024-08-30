<template>
  <div class="grid grid-flow-col h-52">
    <div
      class="h-5/6 mx-6 my-auto border-gray-400 border rounded-md"
      :data-id="guestId"
      v-for="{
        guestName,
        specialNeeds,
        guestId,
        seatLocation,
        remark,
      } in props.guests"
      :key="guestId"
    >
      <div
        class="h-1/4 text-center flex justify-center items-center guest-name"
      >
        {{ guestName }}
      </div>
      <div class="h-3/4 w-full text-center">
        <div class="grid grid-cols-2 py-1 seat">
          <div class="label">宴席座位</div>
          <div class="content">{{ seatLocation?.tableAlias ?? "" }}</div>
        </div>
        <div class="grid grid-cols-2 py-1 special-needs">
          <div class="label">特殊需求</div>
          <div class="content">
            {{ specialNeedContents(specialNeeds).join(",") }}
          </div>
        </div>
        <div class="grid grid-cols-2 py-1 remark">
          <div class="label">備註</div>
          <div class="content">{{ remark }}</div>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped lang="scss"></style>

<script setup lang="ts">
// import type {
//   SingleGuestManagementDetail,
//   GroupGuestManagementDetail,
//   IGuestSpecialNeed,
// } from ".nuxt/imports";

type GuestManagementRowDetailProps = {
  guests: (SingleGuestManagementDetail | GroupGuestManagementDetail)[];
};

const props = defineProps<GuestManagementRowDetailProps>();
const specialNeedContents = (specialNeeds: IGuestSpecialNeed[]): string[] =>
  specialNeeds.map((needs) => needs.specialNeedContent);
</script>
