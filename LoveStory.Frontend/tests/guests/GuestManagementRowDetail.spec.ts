import { beforeEach, describe, expect, it } from "vitest";
import { mount, VueWrapper } from "@vue/test-utils";
import GuestManagementRowDetail from "../../components/guests/GuestManagementRowDetail.vue";
import type { GuestManagement } from "pages/Guests.vue";

describe('Test Guest Management Table Row Detail Display Text', () => {
  let wrapper: VueWrapper<any>;
  const exceptGuests: GuestManagement[] = [
    {
      guestId: "2",
      guestName: "蔣小花",
      guestRelationship: "男方/家人",
      attendanceList: [],
      specialNeeds: ["素食"],
      createTime: new Date(),
      creator: "admin",
      remark: "hello",
      seat: "25 桌"
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
      seat: "24 桌"
    },
    {
      guestId: "4",
      guestName: "蔣小花-2",
      guestRelationship: "男方/家人",
      attendanceList: [],
      specialNeeds: ["兒童椅"],
      createTime: new Date(),
      creator: "admin",
      remark: "",
      seat: "33 桌"
    },
  ];

  beforeEach(() => {
    wrapper = mount(GuestManagementRowDetail, { props: { guests: exceptGuests } });
  });

  it.each(exceptGuests)('Give a specific attendance(guestId: $guestId, guestName: $guestName), should be displayed corrent guest name text', ({ guestId, guestName }) => {
    const guestNameElement = wrapper.find(`div[data-id="${guestId}"] .guest-name`);
    expect(guestNameElement.text()).toBe(guestName);
  });

  it.each(exceptGuests)('Give a specific attendance(guestId: $guestId, specialNeeds: $specialNeeds), should be displayed corrent special needs text', ({ guestId, specialNeeds }) => {
    const specialNeedsContentElement = wrapper.find(`div[data-id="${guestId}"] .special-needs>.content`);
    expect(specialNeedsContentElement.text()).toBe(specialNeeds.join(","));
  });

  it.each(exceptGuests)('Give a specific attendance(guestId: $guestId, seat: $seat), should be displayed corrent seat text', ({ guestId, seat }) => {
    const seatContentElement = wrapper.find(`div[data-id="${guestId}"] .seat>.content`);
    expect(seatContentElement.text()).toBe(seat);
  });

  it.each(exceptGuests)('Give a specific attendance(guestId: $guestId, remark: $remark), should be displayed corrent remark text', ({ guestId, remark }) => {
    const remarkContentElement = wrapper.find(`div[data-id="${guestId}"] .remark>.content`);
    expect(remarkContentElement.text()).toBe(remark);
  });
});