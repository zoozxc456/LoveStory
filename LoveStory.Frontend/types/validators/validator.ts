export interface IFormDataValidator<T> {
  valid: (data: T) => boolean;
}