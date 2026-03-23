import { Status } from '@/ApiModels/Status';
import { GlobalHelper } from '@/helpers/GlobalHelper';
import { IValidationResult } from '@/interfaces/IValidationResult';

export class StatusViewModel {
  id = -GlobalHelper.getAppUID();
  name = '';
  color = '#000';

  setFromStatus(status: Status): void {
    this.id = status.id;
    this.name = status.name;
    this.color = status.color;
  }

  get v$(): IValidationResult {
    return {
      name: {
        required: {
          $validator: !!this.name,
          $message: 'Nazwa jest wymagana!'
        }
      },
      surname: {
        required: {
          $validator: !!this.color,
          $message: 'Kolor jest wymagany!'
        }
      }
    };
  }
}
