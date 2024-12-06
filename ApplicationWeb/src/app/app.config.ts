import { ApplicationConfig, importProvidersFrom } from '@angular/core';
import { provideRouter } from '@angular/router';

import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MessageService } from 'primeng/api';
import { ToastModule } from 'primeng/toast';
import { routes } from './app.routes';
import { HttpErrorInterceptor } from './Core/Interceptors/http-error.interceptor';
import { HttpRequestInterceptor } from './Core/Interceptors/http-request.interceptor';

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideHttpClient
    (
      withInterceptors([HttpErrorInterceptor, HttpRequestInterceptor])
    ),
    importProvidersFrom(BrowserAnimationsModule), // Necesario para PrimeNG Toast
    importProvidersFrom(ToastModule), // Usar importProvidersFrom para ToastModule
    MessageService // El servicio de mensajes debe estar aquí
  ]
};
