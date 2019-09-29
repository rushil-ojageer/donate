import { Charity } from 'src/app/models/Charity';
import { Component, OnInit } from '@angular/core';
import { CharityService } from 'src/app/services/charity/charity.service';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { CreateUpdateMode } from 'src/app/models/CreateUpdateMode';
import { faSpinner } from '@fortawesome/free-solid-svg-icons';
import { ICreateUpdateComponent } from 'src/app/shared/crud-screen/generic-crud-add-update/helper-classes/ICreateUpdateComponent';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-charity-list-add-update',
  templateUrl: './charity-list-add-update.component.html',
  styleUrls: ['./charity-list-add-update.component.less']
})
export class CharityListAddUpdateComponent implements OnInit, ICreateUpdateComponent<Charity> {

  charity = new Charity();
  mode: CreateUpdateMode = CreateUpdateMode.Create;
  createUpdateComponent = this;

  constructor(private modalRef: BsModalRef,
              private charityService: CharityService) { }

  ngOnInit() {
  }

  getTitle(): string {
    if (this.mode === CreateUpdateMode.Update) {
      return 'Edit Charity';
    }
    return 'Add Charity';
  }

  createOrUpdate(mode: CreateUpdateMode, item: Charity): Observable<any> {
    return this.charityService.createOrUpdate(mode, item);
  }
}
