import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DonorDonationsComponent } from './donor-donations.component';

describe('DonorDonationsComponent', () => {
  let component: DonorDonationsComponent;
  let fixture: ComponentFixture<DonorDonationsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DonorDonationsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DonorDonationsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
