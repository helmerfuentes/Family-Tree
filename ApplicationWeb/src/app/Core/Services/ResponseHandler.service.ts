import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { ApiResponse } from '../Models/ApiResponse';

@Injectable({
  providedIn: 'root'
})
export class ResponseHandlerService {

  constructor() { }

  handleResponse<T>(observable: Observable<ApiResponse<T>>): Observable<T> {
    return observable.pipe(
      map(response => {
        if (!response.success) {
          console.log("error handler");
          
          // this.snackBar.open('Error: ' + (response.errorMessage || 'Unknown error'), 'Close', { duration: 5000 });
          throw new Error(response.errorMessage || 'Unknown error');
        }
        return response.data;
      }),
      catchError((error) => {
        console.log("error handler");
        // this.snackBar.open('Error occurred: ' + (error.message || 'Unknown error'), 'Close', { duration: 5000 });
        return throwError(() => new Error(error.message || 'Unknown error'));
      })
    );
  }
}
