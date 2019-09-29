import { Row } from '../../../../models/DataTable';
import { Component } from '@angular/core';
import { CharityService } from 'src/app/services/charity/charity.service';
import { Charity } from 'src/app/models/Charity';
import { BsModalService } from 'ngx-bootstrap/modal';
import { CharityListAddUpdateComponent } from './charity-list-add-update/charity-list-add-update.component';
import { CreateUpdateMode } from 'src/app/models/CreateUpdateMode';
import { faRibbon, IconDefinition } from '@fortawesome/free-solid-svg-icons';
import { ListComponent } from 'src/app/shared/crud-screen/generic-crud-list/helper-classes/ListComponent';
import { TableAction } from 'src/app/shared/crud-screen/generic-crud-list/generic-crud-list.component';
import { IDeleteComponent } from 'src/app/shared/crud-screen/generic-crud-delete/helper-classes/IDeleteComponent';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-charity-list',
  templateUrl: './charity-list.component.html',
  styleUrls: ['./charity-list.component.less']
})
export class CharityListComponent extends ListComponent<Charity> implements IDeleteComponent<Charity> {

  listComponent = this;

  constructor(private bsModalService: BsModalService,
              private charityService: CharityService) {
                super(bsModalService, charityService);
              }

  getTitle(): string {
    return 'Charities';
  }

  getIcon(): IconDefinition {
    return faRibbon;
  }

  getTableColumns(): string[] {
    return ['Name', 'Contact Person', 'Contact Number', 'Email', 'Updated At'];
  }

  addActions() {
    const editAction = new TableAction();
    editAction.label = 'Edit';
    editAction.action = (x: Charity) => this.listComponent.openEditModal(x);
    this.actions.push(editAction);

    const deleteAction = new TableAction();
    deleteAction.label = 'Delete';
    deleteAction.action = (x: Charity) => this.listComponent.openRemoveModal(x);
    this.actions.push(deleteAction);
  }

  getAddModalContent() {
    return CharityListAddUpdateComponent;
  }

  getEditModalContent() {
    return CharityListAddUpdateComponent;
  }

  getEditModelIntialState(item: Charity) {
    return {
      charity: item,
      mode: CreateUpdateMode.Update
    };
  }

  convertToRow(item: Charity): Row {
    const row = new Row(item);
    row.addCell('Name', item.charityName);
    row.addCell('Contact Person', item.contactPerson);
    row.addCell('Contact Number', item.contactNumber);
    row.addCell('Email', item.emailAddress);
    row.addCell('Updated At', item.updatedAt);
    return row;
  }

  delete(item: Charity): Observable<any> {
    return this.charityService.delete(item);
  }

  getDeleteComponent(): IDeleteComponent<Charity> {
    return this;
  }

  getOptions() {
    return {};
  }

  getAddModelIntialState() {
    return {};
  }

}
