<template>
  <div class="h-screen w-screen p-3 relative">
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
            <div class="absolute top-1/4 w-full h-1/3 px-6">
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
                  @click.stop="authenticateUser"
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

  <div
    class="h-screen w-screen absolute top-0 z-[9999] bg-black/80"
    v-if="isLoading"
  >
    <div class="relative w-full h-full">
      <div class="absolute top-1/2 left-1/2 -translate-x-1/2 -translate-y-1/2">
        <span class="text-white text-2xl font-bold"> Loading ... </span>
      </div>
    </div>
  </div>

  <div
    class="h-screen w-screen absolute top-0 z-[9999] bg-black/80"
    v-if="errorMessageDialogDisplayController.state.isShow"
  >
    <div class="relative w-full h-full">
      <div
        class="absolute top-1/2 left-1/2 -translate-x-1/2 -translate-y-1/2 w-[600px] h-[200px] border border-slate-100 rounded-md bg-rose-100 text-red-700 text-2xl text-center font-bold leading-relaxed"
      >
        <div class="relative w-full h-full flex flex-col justify-center">
          <div
            class="absolute top-4 right-6 hover:cursor-pointer"
            @click="handleCloseErrorMessageDialog"
          >
            <font-awesome-icon
              :icon="['fas', 'xmark']"
              size="1x"
              class="text-white"
            />
          </div>
          <p>Your Username or Password is Incorrect.</p>
          <p>Please Try It Again.</p>
        </div>
      </div>
    </div>
  </div>
  <!-- <div class="min-h-screen bg-pink-50 flex items-center justify-center">
    <div
      class="bg-white p-8 rounded-lg shadow-lg w-full max-w-md border border-pink-100"
    >
      <h2 class="text-3xl font-bold text-pink-700 mb-8 text-center">
        Welcome to Our Wedding
      </h2>

      <form @submit.prevent="handleSubmit">
        <div class="mb-6">
          <label
            for="email"
            class="block text-pink-700 text-sm font-semibold mb-2"
            >Email</label
          >
          <input
            v-model="email"
            type="email"
            id="email"
            placeholder="Enter your email"
            class="w-full px-4 py-2 border rounded-lg focus:outline-none focus:ring-2 focus:ring-pink-300"
            required
          />
        </div>

        <div class="mb-8">
          <label
            for="password"
            class="block text-pink-700 text-sm font-semibold mb-2"
            >Password</label
          >
          <input
            v-model="password"
            type="password"
            id="password"
            placeholder="Enter your password"
            class="w-full px-4 py-2 border rounded-lg focus:outline-none focus:ring-2 focus:ring-pink-300"
            required
          />
        </div>

        <button
          type="submit"
          class="w-full bg-pink-100 text-pink-900 py-2 rounded-lg font-bold hover:bg-pink-200 transition duration-200"
        >
          Login
        </button>
      </form>
    </div>
  </div> -->
</template>

<script setup lang="ts">
import img from "../assets/images/img.png";
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
definePageMeta({ layout: "login-layout" });

const { authFormData, authenticateUser, isLoading, errorMessage } =
  useOriginalAuth();

const errorMessageDialogDisplayController = useDialogDisplayController();

watchEffect(() => {
  if (isLoading.value === false && errorMessage.value !== "") {
    errorMessageDialogDisplayController.onShow();
  }
});

const handleCloseErrorMessageDialog = () => {
  errorMessageDialogDisplayController.onClose();
};
</script>
