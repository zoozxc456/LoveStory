<template>
  <div class="w-full h-full mx-auto">
    <div
      class="header h-[10%] flex justify-center items-center text-2xl relative"
    >
      <h1>禮金管理</h1>
    </div>
    <div class="content h-[89%] max-h-[89%] mx-4">
      <ManagementGiftsGiftManagementTable
        @request:create="handler.onRequestCreateWeddingGift"
        @request:delete="handler.onRequestDeleteWeddingGift"
      />
    </div>

    <ManagementGiftsCreateWeddingGiftDialog
      v-model:display-controller="
        dialogDisplayControllers['create-wedding-gift-dialog']
      "
      v-model:form-data="toBeCreatedWeddingGift"
      @created="onCreated"
    />

    <ManagementGiftsDeleteWeddingGiftDialog
      v-model:display-controller="
        dialogDisplayControllers['delete-wedding-gift-dialog']
      "
      v-model:form-data="toBeDeletedWeddingGift"
      @deleted="onDeleted"
    />
  </div>
</template>

<script setup lang="ts">
import {
  useWeddingGiftManagementStore,
  type CreateWeddingGiftFormData,
  type DeleteWeddingGiftFormData,
} from "stores/management/wedding-gifts/useWeddingGiftManagement";
definePageMeta({ layout: "admin-layout" });

const store = useWeddingGiftManagementStore();

const dialogDisplayControllers = reactive<{
  [key in
    | "create-wedding-gift-dialog"
    | "modify-wedding-gift-dialog"
    | "delete-wedding-gift-dialog"]: IDisplayController;
}>({
  "create-wedding-gift-dialog": useDisplayController(),
  "modify-wedding-gift-dialog": useDisplayController(),
  "delete-wedding-gift-dialog": useDisplayController(),
});

const toBeCreatedWeddingGift = reactive<CreateWeddingGiftFormData>({
  amount: 0,
  giftType: "紅包",
  managementName: "",
  managementType: "single",
  guestRelationship: "",
  managementId: "",
});

const toBeDeletedWeddingGift = reactive<DeleteWeddingGiftFormData>({
  managementId: "",
  managementName: "",
  managementType: "single",
  id: "",
});

const handler = {
  onRequestCreateWeddingGift: (data: IWeddingGiftManagement) => {
    toBeCreatedWeddingGift.managementId = data.managementId;
    toBeCreatedWeddingGift.managementName = data.managementName;
    toBeCreatedWeddingGift.guestRelationship =
      data.attendance[0].guestRelationship;

    dialogDisplayControllers["create-wedding-gift-dialog"].onShow();
  },
  onRequestDeleteWeddingGift: ({
    managementId,
    managementName,
    managementType,
  }: IWeddingGiftManagement) => {
    toBeDeletedWeddingGift.managementId = managementId;
    toBeDeletedWeddingGift.managementName = managementName;
    toBeDeletedWeddingGift.managementType = managementType;

    const weddingGift = store.data
      .find((x) => x.managementId === managementId)
      ?.attendance.find((x) => x.weddingGift !== undefined)
      ?.weddingGift as IWeddingGift;

    toBeDeletedWeddingGift.id = weddingGift.id;

    dialogDisplayControllers["delete-wedding-gift-dialog"].onShow();
  },
};

const onCreated = () => {
  store.refresh();
};

const onDeleted = () => {
  store.refresh();
};
</script>
