import { TestBed } from '@angular/core/testing';

import { DonorTransactionSourceDataSourceService } from './donor-transaction-source-data-source.service';

describe('DonorTransactionSourceDataSourceService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: DonorTransactionSourceDataSourceService = TestBed.get(DonorTransactionSourceDataSourceService);
    expect(service).toBeTruthy();
  });
});
