import { MessageTypeEnum } from '@/enums/MessageTypeEnum';
import { MessageModel } from '@/models/MessageModel';
import { ref } from 'vue';
import { AnimationHelper } from './AnimationHelper';

const messagesData = ref<MessageModel[]>([]);

export class MessageHelper {
  static get messages(): MessageModel[] {
    return messagesData.value;
  }

  private constructor() {}

  static addSuccessMessage(title: string, content: string = ''): void {
    const newMessage = new MessageModel(
      MessageTypeEnum.Success,
      title,
      content
    );
    this.addMessage(newMessage);
  }

  static addErrorMessage(title: string, content: string = ''): void {
    const newMessage = new MessageModel(MessageTypeEnum.Error, title, content);
    this.addMessage(newMessage);
  }

  static addWarningMessage(title: string, content: string = ''): void {
    const newMessage = new MessageModel(
      MessageTypeEnum.Warning,
      title,
      content
    );
    this.addMessage(newMessage);
  }

  static addInfoMessage(title: string, content: string = ''): void {
    const newMessage = new MessageModel(MessageTypeEnum.Info, title, content);
    this.addMessage(newMessage);
  }

  static addMessage({ type, title, content }: MessageModel): void {
    const existingMessage = this.messages.find(
      (msg) =>
        msg.type === type && msg.title === title && msg.content === content
    );
    if (existingMessage) {
      AnimationHelper.zoomElement(`.${existingMessage.uniqClass}`);
      existingMessage.quantity++;
      return;
    }

    const newMessage = new MessageModel(type, title, content);
    if (this.messages.length >= 3) {
      this.removeMessage();
    }
    this.messages.push(newMessage);
    setTimeout(() => {
      this.removeMessage();
    }, 5000);
  }

  private static removeMessage(): void {
    this.messages.shift();
  }
}
