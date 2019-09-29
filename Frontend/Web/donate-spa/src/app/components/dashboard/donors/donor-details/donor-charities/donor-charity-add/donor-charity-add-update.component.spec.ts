import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DonorCharityAddUpdateComponent } from './donor-charity-add-update.component';

describe('DonorCharityAddComponent', () => {
  let component: DonorCharityAddUpdateComponent;
  let fixture: ComponentFixture<DonorCharityAddUpdateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DonorCharityAddUpdateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DonorCharityAddUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
