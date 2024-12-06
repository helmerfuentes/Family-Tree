import { Injectable } from '@angular/core';
import { BaseService } from '../base.service';

@Injectable({
  providedIn: 'root'
})
export class DepartmentService  extends BaseService{

  private CONTROLLER_API = "Department";
  private URLS= {
    departaments: `${this.CONTROLLER_API}/departments`,
  }

  // constructor(private http: HttpClient, private responseHandler: ResponseHandlerService) { }
  
  // getDepartmentsAsync(): Promise<Department[]> {
  //   return this.getAsync(this.apiUrl) 
  //   // this.responseHandler.handleResponse(this.http.get<ApiResponse<Department[]>>(this.apiUrl));
  // }
}
