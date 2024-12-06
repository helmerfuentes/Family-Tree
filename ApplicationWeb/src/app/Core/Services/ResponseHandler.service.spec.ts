/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { ResponseHandlerService } from './ResponseHandler.service';

describe('Service: ResponseHandler', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ResponseHandlerService]
    });
  });

  it('should ...', inject([ResponseHandlerService], (service: ResponseHandlerService) => {
    expect(service).toBeTruthy();
  }));
});
