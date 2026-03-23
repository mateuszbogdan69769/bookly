import { IValidationUnit } from './interfaces/IValidationUnit';

declare module '@vue/runtime-core' {
  interface ComponentCustomProperties {
    $validMsg: (value: IValidationUnit) => true | string;
  }
}
