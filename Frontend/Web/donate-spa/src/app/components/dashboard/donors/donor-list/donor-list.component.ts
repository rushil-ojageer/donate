import { Component, OnInit } from '@angular/core';
import { Donor } from 'src/app/models/Donor';
import { DonorService } from 'src/app/services/donor/donor.service';
import { BsModalService } from 'ngx-bootstrap/modal';
import { DonorListAddUpdateComponent } from './donor-list-add-update/donor-list-add-update.component';
import { faDonate, IconDefinition } from '@fortawesome/free-solid-svg-icons';
import { CreateUpdateMode } from 'src/app/models/CreateUpdateMode';
import { Observable } from 'rxjs';
import { ListComponent } from 'src/app/shared/crud-screen/generic-crud-list/helper-classes/ListComponent';
import { IDeleteComponent } from 'src/app/shared/crud-screen/generic-crud-delete/helper-classes/IDeleteComponent';
import { TableAction } from 'src/app/shared/crud-screen/generic-crud-list/generic-crud-list.component';
import { Row } from 'src/app/models/DataTable';
import { Router } from '@angular/router';

@Component({
  selector: 'app-donor-list',
  templateUrl: './donor-list.component.html',
  styleUrls: ['./donor-list.component.less']
})
export class DonorListComponent extends ListComponent<Donor> implements IDeleteComponent<Donor> {

  listComponent = this;

  constructor(private bsModalService: BsModalService,
              private donorService: DonorService,
              private router: Router) {
                super(bsModalService, donorService);
              }

  getTitle(): string {
    return 'Donors';
  }

  getIcon(): IconDefinition {
    return faDonate;
  }

  getTableColumns(): string[] {
    return ['Name', 'Identity Type', 'Identity Number', 'Contact Number', 'Email', 'Updated At'];
  }

  addActions(): void {
    const editAction = new TableAction();
    editAction.label = 'Edit';
    editAction.action = (x: Donor) => this.listComponent.openEditModal(x);
    this.actions.push(editAction);

    const deleteAction = new TableAction();
    deleteAction.label = 'Delete';
    deleteAction.action = (x: Donor) => this.listComponent.openRemoveModal(x);
    this.actions.push(deleteAction);

    const viewAction = new TableAction();
    viewAction.label = 'View';
    viewAction.action = (x: Donor) => this.router.navigateByUrl(`/dashboard/donors/${x.id}/details`);
    this.actions.push(viewAction);
  }

  getAddModalContent() {
    return DonorListAddUpdateComponent;
  }

  getEditModalContent() {
    return DonorListAddUpdateComponent;
  }

  getEditModelIntialState(item: Donor) {
    return {
      donor: item,
      mode: CreateUpdateMode.Update
    };
  }

  getAddModelIntialState() {
    return {};
  }

  getDeleteComponent(): IDeleteComponent<Donor> {
    return this;
  }

  convertToRow(item: Donor): Row {
    const row = new Row(item);
    row.addCell('Name', `${item.firstName} ${item.lastName}`);
    row.addCell('Identity Type', item.identityType);
    row.addCell('Identity Number', item.identityNumber);
    row.addCell('Contact Number', item.contactNumber);
    row.addCell('Email', item.emailAddress);
    row.addCell('Updated At', item.updatedAt);
    return row;
  }

  delete(item: Donor): Observable<any> {
    return this.donorService.delete(item);
  }

  getOptions() {
    return {};
  }
}
