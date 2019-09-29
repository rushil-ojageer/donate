import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CharityListAddUpdateComponent } from './charity-list-add-update.component';

describe('CharityListAddUpdateComponent', () => {
  let component: CharityListAddUpdateComponent;
  let fixture: ComponentFixture<CharityListAddUpdateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CharityListAddUpdateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CharityListAddUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
