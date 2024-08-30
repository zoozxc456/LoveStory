<template>
  <div
    v-if="displayController.state.isShow"
    class="h-full w-full fixed top-0 left-0 z-[999] bg-black/80 backdrop-blur-sm transition-opacity duration-300"
  >
    <div class="relative w-full h-full">
      <div
        class="p-3 flex flex-col rounded-xl bg-pink-50 bg-clip-border text-gray-700 shadow-md"
        :class="[
          'max-h-[600px] min-h-[80dvh] h-4/5 w-4/5',
          'absolute top-1/2 left-1/2 -translate-x-1/2 -translate-y-1/2',
        ]"
      >
        <div class="bg-white rounded-md border h-full w-full">
          <div class="h-[7%]">
            <div class="relative h-full">
              <div class="h-full flex items-center justify-center">
                <h4
                  class="block font-sans text-2xl antialiased font-semibold leading-snug tracking-normal text-blue-gray-900"
                >
                  賓客資訊
                </h4>
              </div>
              <div
                class="w-16 absolute right-0 top-1/2 -translate-y-1/2 flex justify-center items-center cursor-pointer"
                @click="displayController.onClose"
              >
                X
              </div>
            </div>
          </div>

          <div class="body overflow-auto p-3 h-[73%]">
            <div class="flex justify-around items-center pb-3 text-sm">
              <div class="flex-1 text-start px-3 font-bold">賓客名字</div>
              <div class="flex-1">
                {{ guest.guestName }}
              </div>
            </div>
            <div class="flex justify-around items-center pb-3 text-sm">
              <div class="flex-1 text-start px-3 font-bold">賓客關係</div>
              <div class="flex-1">
                {{ guest.guestRelationship }}
              </div>
            </div>
            <div class="flex justify-around items-center pb-3 text-sm">
              <div class="flex-1 text-start px-3 font-bold">參加人數</div>
              <div class="flex-1">
                {{ guest.details.length }}
              </div>
            </div>
            <div class="flex justify-around items-center pb-3 text-sm">
              <div class="flex-1 text-start px-3 font-bold">特殊需求</div>
              <div class="flex-1">
                {{ aggregateSpecialNeedsForGuest(guest) }}
              </div>
            </div>
            <div class="flex justify-around items-center pb-3 text-sm">
              <div class="flex-1 text-start px-3 font-bold">建立時間</div>
              <div class="flex-1">
                {{ dayjs(guest.createAt).format("YYYY-MM-DD HH:mm") }}
              </div>
            </div>
            <div class="flex justify-around items-center pb-3 text-sm">
              <div class="flex-1 text-start px-3 font-bold">建立者</div>
              <div class="flex-1">
                {{ guest.creator.username }}
              </div>
            </div>
            <div class="border border-gray-400 rounded-md p-2">
              <h6
                class="px-3 block font-sans text-lg text-start antialiased font-semibold leading-snug tracking-normal text-blue-gray-900"
              >
                賓客詳細資訊
              </h6>
              <div
                class="flex flex-wrap w-full p-1 pb-0 justify-center items-center gap-3 overflow-y-auto"
              >
                <div
                  class="h-[90%] w-full my-auto border-gray-400 border rounded-md"
                  v-for="{
                    guestName,
                    seatLocation,
                    specialNeeds,
                    remark,
                    guestId,
                  } in guest.details"
                  :key="guestId"
                >
                  <div
                    class="h-1/4 text-center flex justify-center items-center guest-name"
                  >
                    {{ guestName }}
                  </div>
                  <div class="h-3/4 w-full text-center">
                    <div class="flex flex-col">
                      <div class="grid grid-cols-2 py-1 seat">
                        <div class="label">宴席座位</div>
                        <div class="content text-center">
                          {{ seatLocation?.tableAlias }}
                        </div>
                      </div>
                      <div class="grid grid-cols-2 py-1 special-needs">
                        <div class="label">特殊需求</div>
                        <div class="content">
                          {{
                            specialNeeds
                              .map((x) => x.specialNeedContent)
                              .join(",")
                          }}
                        </div>
                      </div>
                      <div class="grid grid-cols-2 py-1 remark">
                        <div class="label">備註</div>
                        <div class="content">{{ remark }}</div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>

          <div class="footer p-3 h-[20%]">
            <div class="flex flex-col justify-center items-center gap-3 h-full">
              <div class="w-full">
                <button
                  type="button"
                  class="w-full py-2 rounded-md border bg-pink-300 text-white hover:bg-pink-100 hover:text-rose-500 hover:border-pink-200 focus:ring-pink-400 active:bg-pink-600 active:text-white transition duration-150 ease-in-out"
                  @click="handlers.onClickModifyButton"
                >
                  修改
                </button>
              </div>
              <div class="w-full">
                <button
                  type="button"
                  class="w-full py-2 rounded-md border border-red-500 text-red-500 hover:bg-red-100 focus:ring-red-400 active:bg-red-500 active:text-white transition duration-150 ease-in-out"
                  @click="handlers.onClickDeleteButton"
                >
                  刪除
                </button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped lang="scss"></style>

<script setup lang="ts">
import dayjs from "dayjs";
import type { GuestManagementTableData } from "../GuestManagementTable.vue";
const requestEvents = inject<{
  onRequestModifyGuest: (guest: GuestManagement) => void;
  onReuestDeleteGuest: (guest: GuestManagement) => void;
}>("request-operator-guest-events");

const displayController = defineModel<IDisplayController>("controller", {
  required: true,
});

const guest = defineModel<GuestManagementTableData>("guest", {
  required: true,
});

const handlers = {
  onClickModifyButton: () => {
    requestEvents?.onRequestModifyGuest(guest.value);
    displayController.value.onClose();
  },
  onClickDeleteButton: () => {
    requestEvents?.onReuestDeleteGuest(guest.value);
    displayController.value.onClose();
  },
};

const unionForSpecialNeeds = (guestManagement: GuestManagement): string[] => {
  const result = guestManagement.details
    .flatMap((x) => x.specialNeeds)
    .map((x) => x.specialNeedContent);
  return [...new Set<string>(result)];
};

const aggregateSpecialNeedsForGuest = (guest: GuestManagement): string => {
  return unionForSpecialNeeds(guest).join(",");
};
</script>
