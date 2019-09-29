import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GenericCrudDeleteComponent } from './generic-crud-delete.component';

describe('GenericCrudDeleteComponent', () => {
  let component: GenericCrudDeleteComponent;
  let fixture: ComponentFixture<GenericCrudDeleteComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GenericCrudDeleteComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GenericCrudDeleteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
