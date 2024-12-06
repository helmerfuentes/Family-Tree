import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { ToastModule } from 'primeng/toast';
import { HeaderComponent } from './components/header/header.component';
import { LoadingComponent } from './components/loading/loading.component';
import { SnackbarComponent } from './components/snackbar/snackbar.component';

@Component({
  selector: 'app-root', 
  standalone: true,
  imports: [
    CommonModule,
    RouterOutlet,  
    ToastModule,
    CardModule,
    ButtonModule,
    SnackbarComponent,
    HeaderComponent,
    LoadingComponent
  ],
  providers:[
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  constructor(){}
}
