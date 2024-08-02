import { beforeEach, describe, expect, it } from "vitest";
import { mount, VueWrapper } from "@vue/test-utils";
import type { GiftManagement } from "pages/gifts.vue";
import { GiftsGiftManagementTable } from ".nuxt/components";

describe('Test Gift Management Table Display Text', () => {
  let wrapper: VueWrapper<any>;
  const exceptGifts: GiftManagement[] = [
    {
      guestName: "蔣小花",
      guestRelationship: "男方/親戚",
      giftMoney: 2000.0,
      createTime: new Date("2024-04-30T12:33:44.123"),
      creator: "admin",
    },
  ];

  beforeEach(() => {
    wrapper = mount(GiftsGiftManagementTable, { props: { gifts: exceptGifts } });
  });

  it('Give a specific gift record, should be displayed correct guest name text', () => {
    const guestNameElement = wrapper.find("tr>td:nth-child(2)");
    expect(guestNameElement.text()).toBe("蔣小花");
  });

  it('Give a specific gift record, should be displayed correct guest relation text', () => {
    const guestRelationElement = wrapper.find("tr>td:nth-child(3)");
    expect(guestRelationElement.text()).toBe("男方/親戚");
  });

  it('Give a specific gift record, should be displayed correct gift money text', () => {
    const giftMoneyElement = wrapper.find("tr>td:nth-child(4)");
    expect(giftMoneyElement.text()).toBe("2000");
  });

  it('Give a specific gift record, should be displayed correct create time text', () => {
    const createTimeElement = wrapper.find("tr>td:nth-child(5)");
    expect(createTimeElement.text()).toBe("2024-04-30 12:33");
  });

  it('Give a specific gift record, should be displayed correct creator text', () => {
    const creatorElement = wrapper.find("tr>td:nth-child(6)");
    expect(creatorElement.text()).toBe("admin");
  });
});