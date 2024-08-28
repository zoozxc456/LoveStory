export default defineNuxtRouteMiddleware(async (to, from) => {
  if (hasAccessToken() && isRedirectToLogin(to.path)) return navigateTo('/');

  else if (!hasAccessToken() && !isRedirectToLogin(to.path)) return navigateTo('/login');

  return;
});

const isRedirectToLogin = (path: string) => path.toLowerCase().includes("login");