import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DonorTransactionSourcesComponent } from './donor-transaction-sources.component';

describe('DonorTransactionSourcesComponent', () => {
  let component: DonorTransactionSourcesComponent;
  let fixture: ComponentFixture<DonorTransactionSourcesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DonorTransactionSourcesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DonorTransactionSourcesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
