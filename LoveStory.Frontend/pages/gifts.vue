<template>
  <div class="w-full h-full mx-auto">
    <div class="header h-[10%] flex justify-center items-center text-2xl">
      禮金管理
    </div>
    <div class="content h-[89%] max-h-[89%] mx-4">
      <GiftsGiftManagementTable :gifts="gifts" />
    </div>
  </div>
</template>

<script setup lang="ts">
definePageMeta({ layout: "admin-layout" });
export type GiftManagement = {
  guestName: string;
  guestRelationship: string;
  giftMoney: number;
  createTime: Date;
  creator: string;
};

const gifts = ref<GiftManagement[]>([
  {
    guestName: "蔣小花",
    guestRelationship: "男方/親戚",
    giftMoney: 2000.0,
    createTime: new Date(),
    creator: "admin",
  },
]);
</script>
