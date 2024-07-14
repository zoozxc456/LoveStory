// https://nuxt.com/docs/api/configuration/nuxt-config

export default defineNuxtConfig({
  modules: ['@nuxt/test-utils/module'],
  compatibilityDate: '2024-04-03',
  devtools: { enabled: true },
  css: ['~/assets/css/main.css'],
  postcss: {
    plugins: {
      tailwindcss: {},
      autoprefixer: {},
    },
  },
  alias: {
    "@interfaces/*": "interfaces/*",
    "@enums/*": "enums/*",
    "@components/*": "components/*",
    "@css/*": "assets/css/*",
    "@images/*": "assets/images/*",
    "@layouts/*": "layouts/*"
  }, runtimeConfig: {}
});
