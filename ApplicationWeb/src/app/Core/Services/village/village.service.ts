import { Injectable } from '@angular/core';
import { ApiResponse } from '../../Models/ApiResponse';
import { BaseService } from '../base.service';

@Injectable({
  providedIn: 'root'
})
export class VillageService extends BaseService {

  private CONTROLLER_API = "Village";
  private URLS = {
    villages: `${this.CONTROLLER_API}/ByCity`,
  }

  GetVillagesByCityIdAsync(cityId: number) {
    return this.getAsync<ApiResponse>(`${this.URLS.villages}/${cityId}`)
  }
}
