import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DonorTransactionSourceAddComponent } from './donor-transaction-source-add.component';

describe('DonorTransactionSourceAddComponent', () => {
  let component: DonorTransactionSourceAddComponent;
  let fixture: ComponentFixture<DonorTransactionSourceAddComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DonorTransactionSourceAddComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DonorTransactionSourceAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
