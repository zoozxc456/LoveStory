import { describe, expect, it } from "vitest";
import { mount } from "@vue/test-utils";
import { CommonNavigationBar } from ".nuxt/components";

describe('Test NavigationBar RouterLink', () => {
  it('should be not contained to /abc', () => {
    const wrapper = mount(CommonNavigationBar);
    expect(wrapper.find("routerlink[to='/abc']").exists()).toBe(false);
  });

  it('should be contained to /', () => {
    const wrapper = mount(CommonNavigationBar);
    expect(wrapper.find("routerlink[to='/']").exists()).toBe(true);
  });


  it('should be contained to /gifts', () => {
    const wrapper = mount(CommonNavigationBar);
    expect(wrapper.find("routerlink[to='/gifts']").exists()).toBe(true);
  });

  it('should be contained to /guests', () => {
    const wrapper = mount(CommonNavigationBar);
    expect(wrapper.find("routerlink[to='/guests']").exists()).toBe(true);
  });
});
