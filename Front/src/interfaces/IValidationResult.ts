import { IValidationUnit } from './IValidationUnit';

export type IValidationResult = {
  [key: string]: {
    [key: string]: IValidationUnit;
  };
};
