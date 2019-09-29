import { TestBed } from '@angular/core/testing';

import { DonorTransactionSourceService } from './donor-transaction-source.service';

describe('DonorTransactionSourceService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: DonorTransactionSourceService = TestBed.get(DonorTransactionSourceService);
    expect(service).toBeTruthy();
  });
});
