import { Component, OnInit, Input } from '@angular/core';
import { faSpinner } from '@fortawesome/free-solid-svg-icons';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { IDeleteComponent } from './helper-classes/IDeleteComponent';

@Component({
  selector: 'app-generic-crud-delete',
  templateUrl: './generic-crud-delete.component.html',
  styleUrls: ['./generic-crud-delete.component.less']
})
export class GenericCrudDeleteComponent implements OnInit {

  @Input() deleteComponent: IDeleteComponent<any>;
  @Input() model: any;

  faSpinner = faSpinner;
  submitting = false;
  errors = [];

  constructor(private modalRef: BsModalRef) { }

  ngOnInit() {
    console.log(this.deleteComponent);
  }

  closeModal() {
    this.modalRef.hide();
  }

  onDeleteItem() {
    this.submitting = true;
    this.errors = [];
    this.deleteComponent.delete(this.model).subscribe(result => {
      if (result) {
        this.submitting = false;
        this.closeModal();
      }
    }, (error: any) => {
      this.submitting = false;
      this.errors.push(error.message);
    });
  }


}
