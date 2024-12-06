import { Injectable } from '@angular/core';
import { ApiResponse } from '../../Models/ApiResponse';
import { BaseService } from '../base.service';

@Injectable({
  providedIn: 'root'
})
export class RelationshipService extends BaseService {
  private CONTROLLER_API = "Relationship";
  private URLS = {
    registerChild: `${this.CONTROLLER_API}/RegisterChild`,
    getRelationship: `${this.CONTROLLER_API}/GetRelationship`
  }
  
  RegisterChildAsync(data: any) {
    return this.postAsync<ApiResponse>(this.URLS.registerChild, data);
  }

  GetRelationshipByPersonIdAsync(personId: number, getParents: boolean) {
    return this.getAsync<ApiResponse>(`${this.URLS.getRelationship}/${personId}/${getParents}`);
  }
}