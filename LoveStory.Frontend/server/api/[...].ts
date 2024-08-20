export default defineEventHandler(() => {
  throw createError({
    statusCode: 404,
    statusMessage: 'API Path Not Found'
  })
});