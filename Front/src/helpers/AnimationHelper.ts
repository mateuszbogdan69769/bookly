export class AnimationHelper {
  private static _ShakeClass = 'shake-animation';
  private static _ZoomClass = 'zoom-animation';

  private constructor() {}

  static shakeElement(querySelector: string): void {
    const errorElements = document.querySelectorAll(querySelector);

    errorElements.forEach((element) => {
      element.classList.remove(this._ShakeClass);
      setTimeout(() => {
        element.classList.add(this._ShakeClass);
      }, 100);
    });
  }

  static zoomElement(querySelector: string): void {
    const errorElements = document.querySelectorAll(querySelector);

    errorElements.forEach((element) => {
      element.classList.remove(this._ZoomClass);
      setTimeout(() => {
        element.classList.add(this._ZoomClass);
      }, 300);
    });
  }
}
