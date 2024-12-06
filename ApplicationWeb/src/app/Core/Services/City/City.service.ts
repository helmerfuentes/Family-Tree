import { Injectable } from '@angular/core';
import { ApiResponse } from '../../Models/ApiResponse';
import { BaseService } from '../base.service';

@Injectable({
  providedIn: 'root'
})
export class CityService extends BaseService {
  private CONTROLLER_API = "City";
  private URLS= {
    departaments: `${this.CONTROLLER_API}/ByDepartment`,
  }

  GetCitiesByDepartmentIdAsync(departmentId:number){
    return this.getAsync<ApiResponse>(`${this.URLS.departaments}/${departmentId}`)
  }

}
