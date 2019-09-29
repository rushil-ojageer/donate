import { Component, OnInit, Input } from '@angular/core';
import { BsModalService } from 'ngx-bootstrap/modal';
import { faRibbon, IconDefinition } from '@fortawesome/free-solid-svg-icons';
import { Observable } from 'rxjs';
import { DonorDonationService } from 'src/app/services/donor/donor-donations/donor-donation.service';
import { IDeleteComponent } from 'src/app/shared/crud-screen/generic-crud-delete/helper-classes/IDeleteComponent';
import { Donor } from 'src/app/models/Donor';
import { ListComponent } from 'src/app/shared/crud-screen/generic-crud-list/helper-classes/ListComponent';
import { Donation } from 'src/app/models/Donation';
import { Row } from 'src/app/models/DataTable';

@Component({
  selector: 'app-donor-donations',
  templateUrl: './donor-donations.component.html',
  styleUrls: ['./donor-donations.component.less']
})
export class DonorDonationsComponent extends ListComponent<Donation> implements OnInit {

  private interalDonor: Donor = new Donor();

  @Input()
  set donor(donor: Donor) {
    this.interalDonor = donor;
    this.load(1);
  }

  listComponent = this;

  constructor(private bsModalService: BsModalService,
              private donorDonationService: DonorDonationService) {
                super(bsModalService, donorDonationService);
              }

  getTitle(): string {
    return 'Donations';
  }

  getIcon(): IconDefinition {
    return faRibbon;
  }

  getTableColumns(): string[] {
    return ['Charity', 'Merchant', 'Transaction Date', 'Transaction Amount', 'Donation Date', 'Donation Amount'];
  }

  ngOnInit() {
    this.supportsCreateUpdate = false;
  }

  getAddModalContent() {
  }

  getEditModalContent() {
  }

  getEditModelIntialState(item: Donation) {
  }

  getAddModelIntialState() {
  }

  getDeleteComponent(): IDeleteComponent<Donation> {
    return null;
  }

  convertToRow(item: Donation): Row {
    const row = new Row(item);
    row.addCell('Charity', item.charity);
    row.addCell('Merchant', item.merchant);
    row.addCell('Transaction Date', item.transactionDateTimeUtc);
    row.addCell('Transaction Amount', item.transactionAmount);
    row.addCell('Donation Date', item.donationDateTimeUtc);
    row.addCell('Donation Amount', item.amount);
    return row;
  }

  addActions(): void {
  }

  delete(item: Donation): Observable<any> {
    return null;
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
