export class LinkHelper {
  private constructor() {}

  static ensureSlashAtEnd(url: string): string {
    return url.endsWith('/') ? url : url + '/';
  }
}
