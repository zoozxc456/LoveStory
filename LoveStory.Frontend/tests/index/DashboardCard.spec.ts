import { describe, expect, it } from "vitest";
import { mount } from "@vue/test-utils";
import DashboardCard from "../../components/index/DashboardCard.vue";

describe('Test Dashboard Card Display Text', () => {
  it('Give a specific header text, should be displayed correct header text', () => {
    const wrapper = mount(DashboardCard, {
      props: {
        header: "已邀請",
        body: ["352 位賓客", "(男方: 100 位 / 女方: 100 位)"]
      }
    });

    const header = wrapper.find(".header");
    expect(header.text()).toBe("已邀請");
  });

  it('Give a specific body text array, should be displayed correct body', () => {
    const wrapper = mount(DashboardCard, {
      props: {
        header: "已邀請",
        body: ["352 位賓客", "(男方: 100 位 / 女方: 100 位)"]
      }
    });

    const body = wrapper.find(".body");
    expect(body.text()).toContain("352 位賓客");
    expect(body.text()).toContain("(男方: 100 位 / 女方: 100 位)");
  });
});