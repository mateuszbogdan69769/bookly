export function setWindowTitle(title = ''): void {
  if (title) {
    document.title = `${title} - Booking App`;
    return;
  }
  document.title = 'Booking App';
}
