import { Component, OnInit, Input, Pipe, PipeTransform } from '@angular/core';
import { Donor } from 'src/app/models/Donor';
import { BsModalService } from 'ngx-bootstrap/modal';
import { faRibbon, IconDefinition } from '@fortawesome/free-solid-svg-icons';
import { ListComponent } from 'src/app/shared/crud-screen/generic-crud-list/helper-classes/ListComponent';
import { IDeleteComponent } from 'src/app/shared/crud-screen/generic-crud-delete/helper-classes/IDeleteComponent';
import { Observable } from 'rxjs';
import { Row } from 'src/app/models/DataTable';
import { DonorCharityAddUpdateComponent } from './donor-charity-add/donor-charity-add-update.component';
import { CreateUpdateMode } from 'src/app/models/CreateUpdateMode';
import { DonorCharity } from 'src/app/models/DonorCharity';
import { TableAction } from 'src/app/shared/crud-screen/generic-crud-list/generic-crud-list.component';
import { DonorCharityService } from 'src/app/services/donor/donor-charity/donor-charity.service';

@Component({
  selector: 'app-donor-charities',
  templateUrl: './donor-charities.component.html',
  styleUrls: ['./donor-charities.component.less']
})
export class DonorCharitiesComponent extends ListComponent<DonorCharity> implements IDeleteComponent<DonorCharity> {
  private interalDonor: Donor = new Donor();

  @Input()
  set donor(donor: Donor) {
    this.interalDonor = donor;
    this.load(1);
  }

  listComponent = this;

  constructor(private bsModalService: BsModalService,
              private donorCharityService: DonorCharityService) {
                super(bsModalService, donorCharityService);
              }

  getTitle(): string {
    return 'Charities';
  }

  getIcon(): IconDefinition {
    return faRibbon;
  }

  getTableColumns(): string[] {
    return ['Charity', 'Percentage'];
  }

  getAddModalContent() {
    return DonorCharityAddUpdateComponent;
  }

  getEditModalContent() {
    return DonorCharityAddUpdateComponent;
  }

  getEditModelIntialState(item: DonorCharity) {
    return {
      donor: this.interalDonor,
      donorCharity: item,
      mode: CreateUpdateMode.Update
    };
  }

  getAddModelIntialState() {
    return {donor: this.interalDonor};
  }

  getDeleteComponent(): IDeleteComponent<DonorCharity> {
    return this;
  }

  convertToRow(item: DonorCharity): Row {
    const row = new Row(item);
    row.addCell('Charity', item.charityName);
    row.addCell('Percentage', item.donationPercentage);
    return row;
  }

  addActions(): void {
    const editAction = new TableAction();
    editAction.label = 'Edit';
    editAction.action = (x: DonorCharity) => this.listComponent.openEditModal(x);
    this.actions.push(editAction);

    const deleteAction = new TableAction();
    deleteAction.label = 'Delete';
    deleteAction.action = (x: DonorCharity) => this.listComponent.openRemoveModal(x);
    this.actions.push(deleteAction);
  }

  delete(item: DonorCharity): Observable<any> {
    return this.donorCharityService.delete(item, this.getOptions());
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
