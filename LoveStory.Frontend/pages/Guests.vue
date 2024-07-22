<template>
  <div class="w-full mx-auto">
    <div class="header text-center text-2xl">賓客管理</div>
    <GuestManagementTable :guest-managements="data" />
  </div>
</template>

<style scoped lang="scss"></style>

<script setup lang="ts">
import { reactive } from "vue";
import GuestManagementTable from "../components/guests/GuestManagementTable.vue";
export type GuestManagement = {
  guestId: string;
  guestName: string;
  guestRelationship: string;
  attendanceList: GuestManagement[];
  specialNeeds: string[];
  createTime: Date;
  creator: string;
  remark: string;
  seat: string;
};

const data = reactive<GuestManagement[]>([
  {
    guestId: "1",
    guestName: "蔣小花",
    guestRelationship: "男方/家人",
    attendanceList: [
      {
        guestId: "2",
        guestName: "蔣小花",
        guestRelationship: "男方/家人",
        attendanceList: [],
        specialNeeds: ["素食"],
        createTime: new Date(),
        creator: "admin",
        remark: "hello",
        seat: ""
      },
      {
        guestId: "3",
        guestName: "蔣小花-1",
        guestRelationship: "男方/家人",
        attendanceList: [],
        specialNeeds: ["兒童椅"],
        createTime: new Date(),
        creator: "admin",
        remark: "test",
        seat: ""
      },
      {
        guestId: "4",
        guestName: "蔣小花-2",
        guestRelationship: "男方/家人",
        attendanceList: [],
        specialNeeds: ["兒童椅"],
        createTime: new Date(),
        creator: "admin",
        remark: "no1",
        seat: ""
      },
    ],
    specialNeeds: [],
    createTime: new Date(),
    creator: "admin",
    remark: "",
    seat: ""
  },
  {
    guestId: "5",
    guestName: "蔣小花",
    guestRelationship: "男方/家人",
    attendanceList: [
      {
        guestId: "6",
        guestName: "蔣小花",
        guestRelationship: "男方/家人",
        attendanceList: [],
        specialNeeds: ["素食"],
        createTime: new Date(),
        creator: "admin",
        remark: "jeellle",
        seat: ""
      },
      {
        guestId: "7",
        guestName: "蔣小花-1",
        guestRelationship: "男方/家人",
        attendanceList: [],
        specialNeeds: ["兒童椅"],
        createTime: new Date(),
        creator: "admin",
        remark: "kelekor",
        seat: ""
      },
      {
        guestId: "8",
        guestName: "蔣小花-2",
        guestRelationship: "男方/家人",
        attendanceList: [],
        specialNeeds: ["兒童椅"],
        createTime: new Date(),
        creator: "admin",
        remark: "kekekk",
        seat: ""
      },
    ],
    specialNeeds: [],
    createTime: new Date(),
    creator: "admin",
    remark: "",
    seat: ""
  },
]);
</script>{
        guestId: "6",
        guestName: "蔣小花",
        guestRelationship: "男方/家人",
        attendanceList: [],
        specialNeeds: ["素食"],
        createTime: new Date(),
        creator: "admin",
        remark: ""
      }{
        guestId: "7",
        guestName: "蔣小花-1",
        guestRelationship: "男方/家人",
        attendanceList: [],
        specialNeeds: ["兒童椅"],
        createTime: new Date(),
        creator: "admin",
        remark: ""
      }{
        guestId: "8",
        guestName: "蔣小花-2",
        guestRelationship: "男方/家人",
        attendanceList: [],
        specialNeeds: ["兒童椅"],
        createTime: new Date(),
        creator: "admin",
        remark: ""
      }
