import { Injectable } from '@angular/core';
import { GenericCrudService } from 'src/app/shared/crud-service/GenericCrudService';
import { DonorTransactionSource, DonorTransactionSources } from 'src/app/models/DonorTransactionSource';
import { IDataSource } from 'src/app/shared/crud-service/IDataSource';
import { DonorTransactionSourceDataSourceService } from './donor-transaction-source-data-source.service';

@Injectable({
  providedIn: 'root'
})
export class DonorTransactionSourceService extends GenericCrudService<DonorTransactionSource, DonorTransactionSources> {

  constructor(private donorTransactionSourceDataSourceService: DonorTransactionSourceDataSourceService) {
    super();
  }

  getDataSource(): IDataSource<DonorTransactionSource, DonorTransactionSources> {
    return this.donorTransactionSourceDataSourceService;
  }

  mapFields(response: DonorTransactionSource, item: DonorTransactionSource): void {
  }

  getId(item: DonorTransactionSource) {
    return item.id;
  }
}
