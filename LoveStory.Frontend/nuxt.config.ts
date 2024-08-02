export default defineNuxtConfig({
  modules: ['@nuxt/test-utils/module'],
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
      'types',
      'types/*/index.{ts,js,mjs,mts}',
      'types/**'
    ]
  },
  plugins: [{ src: '~/plugins/font-awesome.ts' }],
});
