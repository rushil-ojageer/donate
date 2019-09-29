import { TestBed } from '@angular/core/testing';

import { DonorCharityDataSourceService } from './donor-charity-data-source.service';

describe('DonorCharityDataSourceService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: DonorCharityDataSourceService = TestBed.get(DonorCharityDataSourceService);
    expect(service).toBeTruthy();
  });
});
