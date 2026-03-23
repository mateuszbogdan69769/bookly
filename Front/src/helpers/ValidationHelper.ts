import { IValidationUnit } from '@/interfaces/IValidationUnit';

export class ValidationHelper {
  private constructor() {}

  static validateWithMessage(val: IValidationUnit): true | string {
    return val.$validator || val.$message;
  }

  static isEmailValid(email: string): boolean {
    const emailRegex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
    return emailRegex.test(email);
  }
}
