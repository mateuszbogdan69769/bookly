import { MessageTypeEnum } from '../enums/MessageTypeEnum';
import { GlobalHelper } from '@/helpers/GlobalHelper';

export class MessageModel {
  type: MessageTypeEnum;
  title: string;
  content?: string;

  quantity = 1;
  uniqClass = `message-class-id-${GlobalHelper.getAppUID()}`;

  constructor(type: MessageTypeEnum, title: string, content?: string) {
    this.type = type;
    this.title = title;
    this.content = content;
  }
}
