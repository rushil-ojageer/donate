import { DonorsComponent } from './components/dashboard/donors/donors.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { AboutComponent } from './components/about/about.component';
import { CharitiesComponent } from './components/dashboard/charities/charities.component';
import { DonorListComponent } from './components/dashboard/donors/donor-list/donor-list.component';
import { DonorDetailsComponent } from './components/dashboard/donors/donor-details/donor-details.component';

const routes: Routes = [
  { path: 'dashboard', component: DashboardComponent,
    children: [
      { path: 'charities', component: CharitiesComponent },
      { path: 'donors', component: DonorsComponent,
        children: [
          { path: '', component: DonorListComponent },
          { path: ':id/details', component: DonorDetailsComponent }
        ]}
    ]
  },
  { path: 'about', component: AboutComponent },
  { path: '', redirectTo: 'dashboard/donors', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
