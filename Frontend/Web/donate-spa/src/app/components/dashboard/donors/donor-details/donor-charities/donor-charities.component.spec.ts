import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DonorCharitiesComponent } from './donor-charities.component';

describe('DonorCharitiesComponent', () => {
  let component: DonorCharitiesComponent;
  let fixture: ComponentFixture<DonorCharitiesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DonorCharitiesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DonorCharitiesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
