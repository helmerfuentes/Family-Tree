import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { MessageService } from 'primeng/api'; // Importa el servicio de mensajes
import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

export const HttpErrorInterceptor: HttpInterceptorFn = (req, next) => {
  const messageService = inject(MessageService); // Inyecta MessageService
  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {
      // Manejar errores
      let errorMessage: string;
      if (error.error instanceof ErrorEvent) {
        errorMessage = `Client-side error: ${error.error.message}`;
      } else {
        errorMessage = `Server error: ${error?.status}: ${error?.error?.details}`;
      }
      messageService.add({
        severity: 'error',
        summary: 'Error',
        detail: errorMessage
      });

      return throwError(() => new Error(errorMessage));
    })
  );
};
