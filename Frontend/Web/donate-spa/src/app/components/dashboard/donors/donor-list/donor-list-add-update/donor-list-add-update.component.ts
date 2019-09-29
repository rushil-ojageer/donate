import { BsModalRef } from 'ngx-bootstrap/modal';
import { Component, OnInit } from '@angular/core';
import { Donor } from 'src/app/models/Donor';
import { DonorService } from 'src/app/services/donor/donor.service';
import { ICreateUpdateComponent } from 'src/app/shared/crud-screen/generic-crud-add-update/helper-classes/ICreateUpdateComponent';
import { CreateUpdateMode } from 'src/app/models/CreateUpdateMode';
import { Observable } from 'rxjs';


@Component({
  selector: 'app-donor-list-add-update',
  templateUrl: './donor-list-add-update.component.html',
  styleUrls: ['./donor-list-add-update.component.less']
})
export class DonorListAddUpdateComponent implements ICreateUpdateComponent<Donor> {

  donor = new Donor();
  mode: CreateUpdateMode = CreateUpdateMode.Create;
  createUpdateComponent = this;

  constructor(private modelRef: BsModalRef,
              private donorService: DonorService) { }

  getTitle(): string {
    if (this.mode === CreateUpdateMode.Update) {
      return 'Edit Donor';
    }
    return 'Add Donor';
  }

  createOrUpdate(mode: CreateUpdateMode, item: Donor): Observable<boolean> {
    console.log(item);
    return this.donorService.createOrUpdate(mode, item);
  }
}
