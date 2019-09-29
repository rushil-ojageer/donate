import { CreateUpdateMode } from 'src/app/models/CreateUpdateMode';
import { Component, OnInit, Input } from '@angular/core';
import { faSpinner } from '@fortawesome/free-solid-svg-icons';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ICreateUpdateComponent } from 'src/app/shared/crud-screen/generic-crud-add-update/helper-classes/ICreateUpdateComponent';

@Component({
  selector: 'app-generic-crud-add-update',
  templateUrl: './generic-crud-add-update.component.html',
  styleUrls: ['./generic-crud-add-update.component.less']
})
export class GenericCrudAddUpdateComponent implements OnInit {

  @Input() createUpdateComponent: ICreateUpdateComponent<any>;
  @Input() model: any;
  @Input() mode: CreateUpdateMode;

  title: string;
  faSpinner = faSpinner;
  submitting = false;
  errors = [];

  constructor(private modalRef: BsModalRef) { }

  ngOnInit() {
    this.title = this.createUpdateComponent.getTitle();
  }

  closeModal() {
    this.modalRef.hide();
  }

  onSubmit() {
    this.submitting = true;
    this.errors = [];

    this.createUpdateComponent.createOrUpdate(this.mode, this.model).subscribe(result => {
      if (result) {
        this.submitting = false;
        this.closeModal();
      }
    }, (error: any) => {
      this.submitting = false;
      const errors = error.error.errors;
      const errorKeys = Object.keys(errors);
      errorKeys.forEach(key => {
        const allErrors = errors[key];
        allErrors.forEach(e => {
          this.errors.push(e);
        });
      });
    });
  }
}
