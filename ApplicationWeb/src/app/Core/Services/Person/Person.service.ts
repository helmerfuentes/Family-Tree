import { Injectable } from '@angular/core';
import { ApiResponse } from '../../Models/ApiResponse';
import { BaseService } from '../base.service';

@Injectable({
  providedIn: 'root'
})
export class PersonService extends BaseService {
  private CONTROLLER_API = "Person";
  private URLS = {
    getByFilter: `${this.CONTROLLER_API}/GetPersonByFilter`,
    getFullInformation: `${this.CONTROLLER_API}/GetFullInformationById`,
    register: `${this.CONTROLLER_API}/Register`,
  }

  GetPersonByFilterAsync(filter: string, value:string) {
    return this.getAsync<ApiResponse>(`${this.URLS.getByFilter}/${filter}/${value}`);
  }

  GetFullInformationByIdAsync(id: number) {
    return this.getAsync<ApiResponse>(`${this.URLS.getFullInformation}/${id}`);
  }

  RegisterAsync(data: any) {
    return this.postAsync<ApiResponse>(this.URLS.register, data);
  }
}
