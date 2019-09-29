import { Component, OnInit } from '@angular/core';
import { DonorService } from 'src/app/services/donor/donor.service';
import { ActivatedRoute } from '@angular/router';
import { Donor } from 'src/app/models/Donor';
import { faSpinner } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-donor-details',
  templateUrl: './donor-details.component.html',
  styleUrls: ['./donor-details.component.less']
})
export class DonorDetailsComponent implements OnInit {

  id: number;
  donor: Donor;
  loading: boolean;
  faSpinner = faSpinner;

  constructor(private donorService: DonorService,
              private route: ActivatedRoute) {}

  ngOnInit() {
    this.loading = true;
    this.donor = new Donor();
    this.id = +this.route.snapshot.paramMap.get('id');
    this.donorService.getById(this.id).subscribe(donor => {
      this.donor = donor;
      this.loading = false;
    });
  }
}
