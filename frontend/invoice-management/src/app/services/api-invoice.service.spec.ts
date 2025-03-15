import { TestBed } from '@angular/core/testing';

import { ApiInvoiceService } from './api-invoice.service';

describe('ApiInvoiceService', () => {
  let service: ApiInvoiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ApiInvoiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
