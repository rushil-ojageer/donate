import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DonorListAddUpdateComponent } from './donor-list-add-update.component';

describe('DonorListAddUpdateComponent', () => {
  let component: DonorListAddUpdateComponent;
  let fixture: ComponentFixture<DonorListAddUpdateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DonorListAddUpdateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DonorListAddUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
