<template>
  <div class="h-dvh w-dvw p-3 relative">
    <div class="w-full h-full bg-pink-100 rounded-lg lg:flex">
      <div class="w-full lg:w-1/2 h-full relative overflow-hidden">
        <div
          :class="[
            'w-[90%] mx-auto text-center absolute z-10 top-1/4 left-1/2 -translate-x-1/2 -translate-y-3/4',
            'md:-translate-y-2/3',
            'lg:-translate-y-1/2',
          ]"
        >
          <p
            :class="[
              'font-italianno text-4xl tracking-wider leading-relaxed',
              'max-[360px]:text-3xl max-[360px]:leading-relaxed',
              'sm:text-4xl',
              'md:text-5xl md:leading-relaxed',
              'lg:text-4xl lg:leading-relaxed',
              'xl:text-5xl xl:leading-relaxed',
            ]"
          >
            May your journey together be filled with laughter, love, and endless
            adventures.
          </p>
        </div>

        <div
          :class="[
            'bg-red-200 rounded-full absolute -bottom-6 left-1/2 -translate-x-1/2 w-[400px] h-[400px] overflow-hidden',
            'sm:w-[500px] sm:h-[500px] sm:-bottom-6',
            'md:w-[600px] md:h-[600px] md:-bottom-6',
            'lg:w-[480px] lg:h-[480px] lg:-bottom-6 lg:translate-y-[10%]',
            'xl:-bottom-0  xl:w-[600px] xl:h-[600px]',
          ]"
        >
          <div><img :src="img" /></div>
        </div>
      </div>
      <div
        class="w-full lg:w-1/2 h-full absolute top-0 left-0 z-20 lg:relative"
      >
        <div
          class="w-[90%] h-[90%] m-auto bg-rose-50/[.9] lg:bg-rose-50 absolute top-1/2 left-1/2 -translate-x-1/2 -translate-y-1/2 rounded-lg drop-shadow-md"
        >
          <div class="relative h-full w-full">
            <div class="absolute top-[10%] left-1/2 -translate-x-1/2">
              <h2 class="font-inria-sans text-5xl text-gray-700">Login</h2>
            </div>
            <div class="absolute top-1/4 w-full h-1/3 px-6 z-10">
              <div class="py-5">
                <p class="font-inria-sans text-xl py-1 text-gray-700">
                  Username
                </p>
                <input
                  class="w-full h-full px-3 py-3 text-gray-700 font-sans text-sm font-normal transition-all border rounded-md outline outline-0 placeholder-shown:border focus:border-2 focus:border-gray-900 focus:outline-0 disabled:border-0"
                  placeholder=" "
                  v-model="authFormData.username"
                />
              </div>
              <div class="py-5">
                <p class="font-inria-sans text-xl py-1 text-gray-700">
                  Password
                </p>
                <input
                  class="w-full h-full px-3 py-3 text-gray-700 font-sans text-sm font-normal transition-all border rounded-md outline outline-0 placeholder-shown:border focus:border-2 focus:border-gray-900 focus:outline-0 disabled:border-0"
                  placeholder=" "
                  type="password"
                  v-model="authFormData.password"
                />
              </div>
              <div class="py-5">
                <button
                  class="w-full bg-red-300 h-12 border border-red-300 rounded-sm font-inria-sans text-xl text-white font-bold hover:bg-red-400 hover:border-red-400 active:bg-red-500 active:border-red-500"
                  @click="authenticateUser"
                >
                  Sign In
                </button>
              </div>
            </div>
            <div class="absolute bottom-0 w-full h-1/3">
              <div class="w-full h-full relative">
                <div
                  class="absolute top-1/2 left-1/2 -translate-x-1/2 -translate-y-1/2 w-full"
                >
                  <p class="font-millerstone text-4xl text-center w-full">
                    Love Story
                  </p>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>

  <LoginLoading :is-loading="isLoading" />

  <LoginErrorMessageAlert
    v-model:display-controller="errorMessageAlertDisplayController"
  />
</template>

<script setup lang="ts">
import img from "../assets/images/img.png";
definePageMeta({ layout: "login-layout" });

const { authFormData, authenticateUser, isLoading, errorMessage } =
  useOriginalAuth();

const errorMessageAlertDisplayController = useDisplayController();

watchEffect(() => {
  if (isLoading.value === false && errorMessage.value !== "") {
    errorMessageAlertDisplayController.onShow();
  }
});
</script>
