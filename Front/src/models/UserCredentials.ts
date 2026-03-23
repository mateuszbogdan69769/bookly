import { isEmailTaken } from '@/services/account.service';
import { ValidationHelper } from './../helpers/ValidationHelper';
import { IValidationResult } from '@/interfaces/IValidationResult';

export class UserCredentials {
  name = '';
  email = '';
  password = '';

  emailTaken = false;

  async checkEmailAvailability(): Promise<void> {
    this.emailTaken = await isEmailTaken(this.email);
  }

  get v$(): IValidationResult {
    return {
      name: {
        required: {
          $validator: !!this.name,
          $message: 'Imię jest wymagane'
        }
      },
      email: {
        required: {
          $validator: !!this.email,
          $message: 'E-mail jest wymagany'
        },
        valid: {
          $validator: !!this.email && ValidationHelper.isEmailValid(this.email),
          $message: 'E-mail nie jest prawidłowy'
        },
        taken: {
          $validator: !this.emailTaken,
          $message: 'E-mail jest już zajęty'
        }
      },
      password: {
        required: {
          $validator: !!this.password,
          $message: 'Hasło jest wymagane'
        },
        minLength: {
          $validator: !!this.password && this.password.length >= 6,
          $message: 'Minimalna długość hasła wynosi 6'
        }
      }
    };
  }
}
