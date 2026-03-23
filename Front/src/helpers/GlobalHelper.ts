import { cloneDeep } from 'lodash-es';

export class GlobalHelper {
  private static _currentUID = 1000;

  private constructor() {}

  static deepCopy<T>(objToCopy: T): T {
    return cloneDeep(objToCopy) as T;
  }

  static getAppUID(): number {
    return ++this._currentUID;
  }
}
