<template>
  <div
    v-for="(relationshipKey, index) in relationshipKeys()"
    :key="index"
    class="text-gray-700"
  >
    <h1 class="py-3 text-2xl font-bold">{{ relationshipKey }}</h1>

    <div
      v-for="relationship in relationships(relationshipKey)"
      :key="relationship"
    >
      <h2 class="py-1 text-xl font-semibold">
        {{ `${relationshipKey} / ${relationship}` }}
      </h2>
      <div class="w-full grid grid-cols-4 gap-5 my-3">
        <div
          class="h-[200px] p-3 border rounded text-center text-gray-700 flex flex-col hover:cursor-pointer"
          v-for="guest in partitionGuests(relationshipKey, relationship)"
          :key="guest.targetId"
          @click.prevent.stop="
            emits(
              'view-guest-overview',
              guest.targetId,
              guest.attendanceAmount > 1 ? 'family' : 'single'
            )
          "
        >
          <div class="card-header flex items-center justify-center h-1/5 p-4">
            <h6 class="text-2xl font-bold">{{ guest.guestName }}</h6>
          </div>

          <div class="card-body flex justify-center items-center h-1/2 p-4">
            <div class="flex-[3]">{{ guest.relationship }}</div>
            <div class="flex-1">{{ `${guest.attendanceAmount}人` }}</div>
          </div>

          <div class="card-footer flex items-center justify-center h-1/3 p-4">
            <div v-if="guest.arrivedAt">
              {{ `${dayjs(guest.arrivedAt).format("HH:mm")} 報到了` }}
            </div>
            <button
              v-else
              class="block w-full select-none rounded-lg py-3 px-6 text-center align-middle font-sans text-xl font-bold uppercase text-white bg-pink-300 shadow-md shadow-gray-900/10 transition-all hover:shadow-lg hover:shadow-gray-900/20 active:opacity-[0.85] disabled:pointer-events-none disabled:opacity-50 disabled:shadow-none"
              type="button"
              @click="handleGuestArrive(guest.targetId)"
            >
              報到
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped lang="scss"></style>

<script setup lang="ts">
import dayjs from "dayjs";
import { useRecipientStore } from "stores/recipient/useRecipient";
import { useRecipientGuestFilterStore } from "stores/recipient/useRecipientGuestFilter";

type GuestOverviewCardProps = {
  guests: RecipientGuest[];
  relationships: {
    [key in "全部" | "主婚人" | "男方" | "女方" | "共同朋友"]: string[];
  };
};

const props = defineProps<GuestOverviewCardProps>();
const emits = defineEmits<{
  (
    e: "view-guest-overview",
    targetId: string,
    guestType: "single" | "family"
  ): void;
}>();
const recipientStore = useRecipientStore();
const filterStore = useRecipientGuestFilterStore();

const handleGuestArrive = async (guestId: string) => {
  const attendanceAmount =
    props.guests.find((guest) => guest.targetId === guestId)
      ?.attendanceAmount ?? 0;
  const guestType = attendanceAmount > 1 ? "family" : "single";

  await recipientStore.guestArrive(guestId, guestType);
  await recipientStore.refreshRecipientGuests();
};

const partitionGuests = (key: string, relationship: string) => {
  return props.guests
    .filter((x) => x.relationship.includes(relationship))
    .sort((x, y) => x.guestName.localeCompare(y.guestName));
};

const relationshipKeys = (): ("主婚人" | "男方" | "女方" | "共同朋友")[] => {
  const excludeAll = Object.keys(props.relationships).filter(
    (x) => x !== "全部"
  ) as ("主婚人" | "男方" | "女方" | "共同朋友")[];

  return excludeAll.filter((x) => {
    if (filterStore.filterFields.maleOrFemale === "全部") {
      if (filterStore.filterFields.guestNameSearchText === "") {
        return true;
      }
      return props.guests
        .filter((y) =>
          y.guestName.includes(filterStore.filterFields.guestNameSearchText)
        )
        .some((y) => y.relationship.includes(x));
    }

    return (
      x === filterStore.filterFields.maleOrFemale &&
      props.guests
        .filter((y) =>
          y.guestName.includes(filterStore.filterFields.guestNameSearchText)
        )
        .some((y) => y.relationship.includes(x))
    );
  });
};

const relationships = (key: "主婚人" | "男方" | "女方" | "共同朋友") => {
  return props.relationships[key]
    .filter((x) => x !== "全部")
    .filter((x) => {
      if (filterStore.filterFields.relationship === "全部") {
        if (filterStore.filterFields.guestNameSearchText === "") {
          return true;
        }

        return props.guests
          .filter((y) =>
            y.guestName.includes(filterStore.filterFields.guestNameSearchText)
          )
          .some((y) => y.relationship.includes(x));
      }

      return (
        x === filterStore.filterFields.relationship &&
        props.guests
          .filter((y) =>
            y.guestName.includes(filterStore.filterFields.guestNameSearchText)
          )
          .some((y) => y.relationship.includes(x))
      );
    })
    .sort((x, y) => {
      if (x.includes("不出席賓客")) return 1;
      if (y.includes("不出席賓客")) return -1;

      return x.localeCompare(y);
    });
};
</script>
