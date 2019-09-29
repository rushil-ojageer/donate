import { Component, OnInit } from '@angular/core';
import { ICreateUpdateComponent } from 'src/app/shared/crud-screen/generic-crud-add-update/helper-classes/ICreateUpdateComponent';
import { DonorCharity } from 'src/app/models/DonorCharity';
import { CreateUpdateMode } from 'src/app/models/CreateUpdateMode';
import { Observable, Subject } from 'rxjs';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { DonorCharityService } from 'src/app/services/donor/donor-charity/donor-charity.service';
import { Donor } from 'src/app/models/Donor';
import { CharityService } from 'src/app/services/charity/charity.service';
import { TypeaheadMatch } from 'ngx-bootstrap/typeahead/public_api';
import { mergeMap } from 'rxjs/operators';

@Component({
  selector: 'app-donor-charity-add',
  templateUrl: './donor-charity-add-update.component.html',
  styleUrls: ['./donor-charity-add-update.component.less']
})
export class DonorCharityAddUpdateComponent implements OnInit, ICreateUpdateComponent<DonorCharity> {

  donor: Donor;
  donorCharity = new DonorCharity();
  mode: CreateUpdateMode = CreateUpdateMode.Create;
  createUpdateComponent = this;
  dataSource: Observable<any>;
  typeaheadLoading: boolean;
  asyncSelected: string;

  constructor(private modalRef: BsModalRef,
              private charityService: CharityService,
              private donorCharityService: DonorCharityService) {
                this.dataSource = Observable.create((observer: any) => {
                  observer.next(this.asyncSelected);
                }).pipe(
                  mergeMap((token: string) => {
                    console.log(token);
                    return this.getStatesAsObservable(token);
                  })
                );
              }

  ngOnInit() {
    this.donorCharity.donorId = this.donor.id;
    this.asyncSelected = this.donorCharity.charityName;
  }

  createOrUpdate(mode: CreateUpdateMode, item: DonorCharity): Observable<boolean> {
    return this.donorCharityService.createOrUpdate(mode, item, {donorId: this.donor.id});
  }

  getTitle(): string {
    if (this.mode === CreateUpdateMode.Update) {
      return 'Edit Charity';
    }
    return 'Add Charity';
  }

  getStatesAsObservable(token: string): Observable<any> {
    const subject = new Subject<any>();
    this.charityService.search(token, 1, 30, {}).subscribe(x => {
      subject.next(x.items);
    });
    return subject;
  }

  changeTypeaheadLoading(e: boolean): void {
    this.typeaheadLoading = e;
  }

  typeaheadOnSelect(e: TypeaheadMatch): void {
    this.donorCharity.charityIdentifier = e.item.charityIdentifier;
    console.log(this.donorCharity);
  }
}
