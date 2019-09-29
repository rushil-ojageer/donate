import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { DashboardComponent } from './components/dashboard/dashboard.component';
import { AboutComponent } from './components/about/about.component';
import { CharitiesComponent } from './components/dashboard/charities/charities.component';
import { DonorsComponent } from './components/dashboard/donors/donors.component';

import { ModalModule } from 'ngx-bootstrap/modal';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { AccordionModule } from 'ngx-bootstrap/accordion';
import { TypeaheadModule } from 'ngx-bootstrap/typeahead';

import { DonorDetailsComponent } from './components/dashboard/donors/donor-details/donor-details.component';
// tslint:disable-next-line:max-line-length
import { CharityListComponent } from './components/dashboard/charities/charity-list/charity-list.component';
import { DonorListComponent } from './components/dashboard/donors/donor-list/donor-list.component';
// tslint:disable-next-line:max-line-length
import { DonorTransactionSourcesComponent } from './components/dashboard/donors/donor-details/donor-transaction-sources/donor-transaction-sources.component';
// tslint:disable-next-line:max-line-length
import { DonorCharitiesComponent } from './components/dashboard/donors/donor-details/donor-charities/donor-charities.component';
import { DonorDonationsComponent } from './components/dashboard/donors/donor-details/donor-donations/donor-donations.component';
// tslint:disable-next-line:max-line-length
// tslint:disable-next-line:max-line-length
import { DonorListAddUpdateComponent } from './components/dashboard/donors/donor-list/donor-list-add-update/donor-list-add-update.component';
// tslint:disable-next-line:max-line-length
import { CharityListAddUpdateComponent } from './components/dashboard/charities/charity-list/charity-list-add-update/charity-list-add-update.component';
// tslint:disable-next-line:max-line-length
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { GenericCrudListComponent } from './shared/crud-screen/generic-crud-list/generic-crud-list.component';
import { GenericCrudAddUpdateComponent } from './shared/crud-screen/generic-crud-add-update/generic-crud-add-update.component';
import { GenericCrudDeleteComponent } from './shared/crud-screen/generic-crud-delete/generic-crud-delete.component';
// tslint:disable-next-line:max-line-length
import { DonorCharityAddUpdateComponent } from './components/dashboard/donors/donor-details/donor-charities/donor-charity-add/donor-charity-add-update.component';
// tslint:disable-next-line:max-line-length
import { DonorTransactionSourceAddComponent } from './components/dashboard/donors/donor-details/donor-transaction-sources/donor-transaction-source-add/donor-transaction-source-add.component';
import { PaginationModule } from 'ngx-bootstrap/pagination';

@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    AboutComponent,
    CharitiesComponent,
    DonorsComponent,
    DonorDetailsComponent,
    CharityListComponent,
    DonorListComponent,
    DonorTransactionSourcesComponent,
    DonorCharitiesComponent,
    DonorDonationsComponent,
    DonorListAddUpdateComponent,
    CharityListAddUpdateComponent,
    GenericCrudListComponent,
    GenericCrudAddUpdateComponent,
    GenericCrudDeleteComponent,
    DonorCharityAddUpdateComponent,
    DonorTransactionSourceAddComponent,
  ],
  imports: [
    BrowserAnimationsModule,
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    FontAwesomeModule,
    HttpClientModule,
    ModalModule.forRoot(),
    TabsModule.forRoot(),
    AccordionModule.forRoot(),
    TypeaheadModule.forRoot(),
    PaginationModule.forRoot()
  ],
  providers: [],
  bootstrap: [AppComponent],
  entryComponents: [CharityListAddUpdateComponent,
    DonorListAddUpdateComponent,
    GenericCrudDeleteComponent,
    DonorCharityAddUpdateComponent,
    DonorTransactionSourceAddComponent]
})
export class AppModule { }
