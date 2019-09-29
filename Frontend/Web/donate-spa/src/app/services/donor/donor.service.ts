import { Injectable } from '@angular/core';
import { Donor, Donors } from 'src/app/models/Donor';
import { BehaviorSubject } from 'rxjs';
import { GenericCrudService } from 'src/app/shared/crud-service/GenericCrudService';
import { IDataSource } from 'src/app/shared/crud-service/IDataSource';
import { DonorDataSourceService } from './donor-data-source.service';

@Injectable({
  providedIn: 'root'
})
export class DonorService extends GenericCrudService<Donor, Donors> {

  constructor(private donorDataSourceService: DonorDataSourceService) {
    super();
  }

  getDataSource(): IDataSource<Donor, Donors> {
    return this.donorDataSourceService;
  }

  mapFields(response: Donor, item: Donor): void {
    item.id = response.id;
    item.updatedAt = response.updatedAt;
  }

  getId(item: Donor) {
    return item.id;
  }
}
