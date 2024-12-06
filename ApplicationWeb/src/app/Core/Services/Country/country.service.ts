import { Injectable } from '@angular/core';
import { ApiResponse } from '../../Models/ApiResponse';
import { BaseService } from '../base.service';

@Injectable({
  providedIn: 'root'
})
export class CountryService extends BaseService {
  private CONTROLLER_API = "Country";
  private URLS= {
    departaments: `${this.CONTROLLER_API}/departments`,
  }

  GetAllCountryWithDepartmentsAsync()
  {
    return this.getAsync<ApiResponse>(this.URLS.departaments);
  }
    
}
