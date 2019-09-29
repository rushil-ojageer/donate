import { Injectable } from '@angular/core';
import { GenericDataSource } from 'src/app/shared/crud-service/GenericDataSource';
import { DonorTransactionSource, DonorTransactionSources } from 'src/app/models/DonorTransactionSource';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class DonorTransactionSourceDataSourceService extends GenericDataSource<DonorTransactionSource, DonorTransactionSources> {

  constructor(private httpClient: HttpClient) {
    super(httpClient);
  }

  getBaseUrl(options: any): string {
    return 'http://34.90.158.29/donors/api/';
  }

  getResource(options: any): string {
    return `donor/${options.donorId}/transactionSource`;
  }

}
