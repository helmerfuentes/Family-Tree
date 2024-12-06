import { Component, inject } from '@angular/core';
import { ToastModule } from 'primeng/toast';
import { AppMessageService } from '../../Core/Services/app-message.service';

@Component({
  selector: 'app-snackbar',
  standalone: true,
  imports: [ToastModule],
  templateUrl: './snackbar.component.html',
  styleUrl: './snackbar.component.scss',
})
export class SnackbarComponent {
  private readonly messageService = inject(AppMessageService);

  get mainSnackbarKey(): string {
    return this.messageService.mainSnackbarKey;
  }
}
