import { inject, Injectable } from '@angular/core';
import { MessageService } from 'primeng/api';
import { SnackbarSeverityEnum } from '../Enums/snackbar-severity.enum';

@Injectable({
  providedIn: 'root',
})
export class AppMessageService {
  private readonly messageService = inject(MessageService);
  public readonly mainSnackbarKey = 'snackbar';

  public showSnackbarMessage(
    message: string,
    severity: SnackbarSeverityEnum
  ): void {
    this.messageService.add({
      key: this.mainSnackbarKey,
      severity,
      detail: message,
    });
  }
}
