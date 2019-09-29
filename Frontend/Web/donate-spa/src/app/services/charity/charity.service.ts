import { GenericCrudService } from 'src/app/shared/crud-service/GenericCrudService';
import { Injectable } from '@angular/core';
import { Charity, Charities } from 'src/app/models/Charity';
import { CharityDataSourceService } from './charity-data-source.service';
import { IDataSource } from 'src/app/shared/crud-service/IDataSource';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CharityService extends GenericCrudService<Charity, Charities> {

  constructor(private dataSource: CharityDataSourceService) {
    super();
  }

  getDataSource(): IDataSource<Charity, Charities> {
    return this.dataSource;
  }

  mapFields(response: Charity, item: Charity): void {
    item.charityIdentifier = response.charityIdentifier;
    item.id = response.id;
    item.updatedAt = response.updatedAt;
  }

  search(search: string, page: number, count: number, options: any = null): Observable<Charities> {
    return this.dataSource.search(search, page, count, options);
  }

  getId(item: Charity) {
    return item.id;
  }
}
