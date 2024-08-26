export default defineNuxtConfig({
  modules: ['@nuxt/test-utils/module', '@pinia/nuxt'],
  compatibilityDate: '2024-04-03',
  devtools: { enabled: true },
  css: ['~/assets/css/main.css', '@fortawesome/fontawesome-svg-core/styles.css'],
  postcss: {
    plugins: {
      tailwindcss: {},
      autoprefixer: {},
    },
  },
  imports: {
    dirs: [
      'apis',
      'apis/**',
      'composables',
      'composables/*/index.{ts,js,mjs,mts}',
      'composables/**',
      'utils',
      'utils/*/index.{ts,js,mjs,mts}',
      'utils/**',
      'types',
      'types/*/index.{ts,js,mjs,mts}',
      'types/**'
    ]
  },
  plugins: [{ src: '~/plugins/font-awesome.ts' }],
  runtimeConfig: {
    backendServiceBaseApiAddress: process.env.NUXT_BACKEND_SERVICE_BASE_API_ADDRESS || "http://<lovestory.api.url>",
  },
  build: {
    transpile: ['@fortawesome/vue-fontawesome']
  },

});
