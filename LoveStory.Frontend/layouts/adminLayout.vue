<template>
  <div class="h-dvh w-dvw relative lg:flex lg:gap-2">
    <CommonMobileNavigationBar
      :class="[' absolute top-0 z-10 w-full h-[10%]', 'lg:hidden']"
    />
    <CommonNavigationBar :class="['hidden lg:block lg:w-[15%] lg:h-full']" />
    <div
      :class="[
        'absolute top-[10%] h-[89%] w-full',
        'lg:static lg:w-[85%] lg:h-full',
      ]"
    >
      <slot></slot>
    </div>
  </div>
</template>
