import { TestBed } from '@angular/core/testing';

import { CharityDataSourceService } from './charity-data-source.service';

describe('CharityDataSourceService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: CharityDataSourceService = TestBed.get(CharityDataSourceService);
    expect(service).toBeTruthy();
  });
});
