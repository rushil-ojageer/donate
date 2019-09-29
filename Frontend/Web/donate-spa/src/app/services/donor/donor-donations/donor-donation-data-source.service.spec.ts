import { TestBed } from '@angular/core/testing';

import { DonorDonationDataSourceService } from './donor-donation-data-source.service';

describe('DonorDonationDataSourceService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: DonorDonationDataSourceService = TestBed.get(DonorDonationDataSourceService);
    expect(service).toBeTruthy();
  });
});
