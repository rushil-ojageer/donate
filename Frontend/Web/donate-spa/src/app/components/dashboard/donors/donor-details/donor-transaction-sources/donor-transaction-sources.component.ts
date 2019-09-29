import { Component, OnInit, Input } from '@angular/core';
import { Donor } from 'src/app/models/Donor';
import { DonorTransactionSource } from 'src/app/models/DonorTransactionSource';
import { ListComponent } from 'src/app/shared/crud-screen/generic-crud-list/helper-classes/ListComponent';
import { IDeleteComponent } from 'src/app/shared/crud-screen/generic-crud-delete/helper-classes/IDeleteComponent';
import { BsModalService } from 'ngx-bootstrap/modal';
import { faRibbon, IconDefinition } from '@fortawesome/free-solid-svg-icons';
import { CreateUpdateMode } from 'src/app/models/CreateUpdateMode';
import { Row } from 'src/app/models/DataTable';
import { TableAction } from 'src/app/shared/crud-screen/generic-crud-list/generic-crud-list.component';
import { Observable } from 'rxjs';
import { DonorTransactionSourceAddComponent } from './donor-transaction-source-add/donor-transaction-source-add.component';
import { DonorTransactionSourceService } from 'src/app/services/donor/donor-transaction-source/donor-transaction-source.service';

@Component({
  selector: 'app-donor-transaction-sources',
  templateUrl: './donor-transaction-sources.component.html',
  styleUrls: ['./donor-transaction-sources.component.less']
})
export class DonorTransactionSourcesComponent
       extends ListComponent<DonorTransactionSource>
       implements IDeleteComponent<DonorTransactionSource> {

  private interalDonor: Donor = new Donor();

  @Input()
  set donor(donor: Donor) {
    this.interalDonor = donor;
    this.load(1);
  }

  listComponent = this;

  constructor(private bsModalService: BsModalService,
              private donorTransactionSourceService: DonorTransactionSourceService) {
                super(bsModalService, donorTransactionSourceService);
              }

  getTitle(): string {
    return 'Transaction Sources';
  }

  getIcon(): IconDefinition {
    return faRibbon;
  }

  getTableColumns(): string[] {
    return ['Financial Institution', 'Account Type', 'Account Number'];
  }

  getAddModalContent() {
    return DonorTransactionSourceAddComponent;
  }

  getEditModalContent() {
    return DonorTransactionSourceAddComponent;
  }

  getEditModelIntialState(item: DonorTransactionSource) {
    return {
      donor: this.interalDonor,
      donorTransactionSource: item,
      mode: CreateUpdateMode.Update
    };
  }

  getAddModelIntialState() {
    return {donor: this.interalDonor};
  }

  getDeleteComponent(): IDeleteComponent<DonorTransactionSource> {
    return this;
  }

  convertToRow(item: DonorTransactionSource): Row {
    const row = new Row(item);
    row.addCell('Financial Institution', item.financialInstitution);
    row.addCell('Account Type', item.type);
    row.addCell('Account Number', item.identifier);
    return row;
  }

  addActions(): void {
    const deleteAction = new TableAction();
    deleteAction.label = 'Delete';
    deleteAction.action = (x: DonorTransactionSource) => this.listComponent.openRemoveModal(x);
    this.actions.push(deleteAction);
  }

  delete(item: DonorTransactionSource): Observable<any> {
    return this.donorTransactionSourceService.delete(item, this.getOptions());
  }

  getOptions() {
    return {donorId: this.interalDonor.id};
  }

  canLoad(options: any): boolean {
    if (options.donorId) {
      return true;
    }

    return false;
  }
}
