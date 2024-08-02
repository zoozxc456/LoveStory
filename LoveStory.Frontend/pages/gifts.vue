<template>
  <div class="w-full mx-auto">
    <div class="header text-center text-2xl">禮金管理</div>
    <GiftsGiftManagementTable :gifts="gifts" />
  </div>
</template>

<script setup lang="ts">
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
