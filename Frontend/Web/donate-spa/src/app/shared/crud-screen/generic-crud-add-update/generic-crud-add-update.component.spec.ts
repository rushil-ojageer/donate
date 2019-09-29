import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GenericCrudAddUpdateComponent } from './generic-crud-add-update.component';

describe('GenericCrudAddUpdateComponent', () => {
  let component: GenericCrudAddUpdateComponent;
  let fixture: ComponentFixture<GenericCrudAddUpdateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GenericCrudAddUpdateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GenericCrudAddUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
