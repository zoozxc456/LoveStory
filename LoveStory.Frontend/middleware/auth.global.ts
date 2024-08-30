export default defineNuxtRouteMiddleware(async (to) => {
  if (hasAccessToken() && isRedirectToLogin(to.path)) return navigateTo('/management');

  else if (!hasAccessToken() && !isRedirectToLogin(to.path)) return navigateTo('/login');

  return;
});

const isRedirectToLogin = (path: string) => path.toLowerCase().includes("login");