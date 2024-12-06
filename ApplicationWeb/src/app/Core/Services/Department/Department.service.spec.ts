/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { DepartmentService } from './Department.service';

describe('Service: DepartmentService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [DepartmentService]
    });
  });

  it('should ...', inject([DepartmentService], (service: DepartmentService) => {
    expect(service).toBeTruthy();
  }));
});
