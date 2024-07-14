import { describe, expect, it } from "vitest";
import { mount } from "@vue/test-utils";
import NavigationBar from "../../components/common/NavigationBar.vue";


describe('Test NavigationBar RouterLink', () => {
  it('should be not contained to /abc', () => {
    const wrapper = mount(NavigationBar);
    expect(wrapper.find("routerlink[to='/abc']").exists()).toBe(false);
  });

  it('should be contained to /', () => {
    const wrapper = mount(NavigationBar);
    expect(wrapper.find("routerlink[to='/']").exists()).toBe(true);
  });


  it('should be contained to /gifts', () => {
    const wrapper = mount(NavigationBar);
    expect(wrapper.find("routerlink[to='/gifts']").exists()).toBe(true);
  });

  it('should be contained to /guests', () => {
    const wrapper = mount(NavigationBar);
    expect(wrapper.find("routerlink[to='/guests']").exists()).toBe(true);
  });
});
