import { Component, OnInit } from '@angular/core';
import { DonorTransactionSource } from 'src/app/models/DonorTransactionSource';
import { ICreateUpdateComponent } from 'src/app/shared/crud-screen/generic-crud-add-update/helper-classes/ICreateUpdateComponent';
import { CreateUpdateMode } from 'src/app/models/CreateUpdateMode';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { Observable } from 'rxjs';
import { DonorTransactionSourceService } from 'src/app/services/donor/donor-transaction-source/donor-transaction-source.service';
import { Donor } from 'src/app/models/Donor';

@Component({
  selector: 'app-donor-transaction-source-add',
  templateUrl: './donor-transaction-source-add.component.html',
  styleUrls: ['./donor-transaction-source-add.component.less']
})
export class DonorTransactionSourceAddComponent  implements OnInit, ICreateUpdateComponent<DonorTransactionSource> {

  donor: Donor;
  donorTransactionSource = new DonorTransactionSource();
  mode: CreateUpdateMode = CreateUpdateMode.Create;
  createUpdateComponent = this;

  constructor(private modalRef: BsModalRef,
              private donorTransactionSourceService: DonorTransactionSourceService) { }

  ngOnInit() {
    this.donorTransactionSource.donorId = this.donor.id;

  }

  getTitle(): string {
    if (this.mode === CreateUpdateMode.Update) {
      return 'Edit Transaction Source';
    }
    return 'Add Transaction Source';
  }

  createOrUpdate(mode: CreateUpdateMode, item: DonorTransactionSource): Observable<boolean> {
    return this.donorTransactionSourceService.createOrUpdate(mode, item, {donorId: this.donor.id});
  }

}
