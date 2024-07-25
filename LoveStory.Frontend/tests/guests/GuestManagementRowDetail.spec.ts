import { beforeEach, describe, expect, it } from "vitest";
import { mount, VueWrapper } from "@vue/test-utils";
import GuestManagementRowDetail from "../../components/guests/GuestManagementRowDetail.vue";
import type { GroupGuestManagementDetail, GuestManagement, SingleGuestManagementDetail } from "types/GuestManagement/guestManagement.type";

describe('Test Guest Management Table Row Detail Display Text', () => {
  let wrapper: VueWrapper<any>;
  const exceptDetails: (SingleGuestManagementDetail | GroupGuestManagementDetail)[] = [{
    guestId: "2",
    createAt: new Date(),
    creator: { userId: "aaa", username: "admin" },
    guestName: "蔣小花",
    seatLocation: {
      banquetTableId: "",
      maxSeatAmount: 0,
      minSeatAmount: 0,
      tableAlias: "25桌",
      remark: "",
      createAt: new Date(),
      creator: { userId: "aaa", username: "admin" },
    },
    isAttended: false,
    remark: "",
    specialNeeds: [{
      specialNeedId: "",
      specialNeedContent: "素食",
      guestId: "2",
      createAt: new Date(),
      creator: { userId: "aaa", username: "admin" },
    }],
  }];

  beforeEach(() => {
    wrapper = mount(GuestManagementRowDetail, { props: { guests: exceptDetails } });
  });

  it.each(exceptDetails)('Give a specific attendance(guestId: $guestId, guestName: $guestName), should be displayed corrent guest name text', ({ guestId, guestName }) => {
    const guestNameElement = wrapper.find(`div[data-id="${guestId}"] .guest-name`);
    expect(guestNameElement.text()).toBe(guestName);
  });

  it.each(exceptDetails)('Give a specific attendance(guestId: $guestId, specialNeeds: $specialNeeds, should be displayed corrent special needs text', ({ guestId, specialNeeds }) => {
    const specialNeedsContentElement = wrapper.find(`div[data-id="${guestId}"] .special-needs>.content`);
    expect(specialNeedsContentElement.text()).toBe(specialNeeds.map(x=>x.specialNeedContent).join(","));
  });

  it.each(exceptDetails)('Give a specific attendance(guestId: $guestId, seat: $seatLocation.tableAlias), should be displayed corrent seat text', ({ guestId, seatLocation }) => {
    const seatContentElement = wrapper.find(`div[data-id="${guestId}"] .seat>.content`);
    expect(seatContentElement.text()).toBe(seatLocation.tableAlias);
  });

  it.each(exceptDetails)('Give a specific attendance(guestId: $guestId, remark: $remark), should be displayed corrent remark text', ({ guestId, remark }) => {
    const remarkContentElement = wrapper.find(`div[data-id="${guestId}"] .remark>.content`);
    expect(remarkContentElement.text()).toBe(remark);
  });
});