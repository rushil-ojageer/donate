import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GenericCrudListComponent } from './generic-crud-list.component';

describe('GenericCrudListComponent', () => {
  let component: GenericCrudListComponent;
  let fixture: ComponentFixture<GenericCrudListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GenericCrudListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GenericCrudListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
