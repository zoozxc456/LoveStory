export const content = [
  "./components/**/*.{js,vue,ts}",
  "./layouts/**/*.vue",
  "./pages/**/*.vue",
  "./plugins/**/*.{js,ts}",
  "./app.vue",
  "./error.vue",
];
export const theme = {
  fontFamily: {
    italianno: ['"Italianno"', "cursive"],
    "inria-sans": ['"Inria Sans"', "sans-serif"],
    millerstone: ['"Millerstone"', "cursive"],
  },
  extend: {
    screens: {
      mini: { max: "360px" },
    },
  },
};
export const plugins = [];
