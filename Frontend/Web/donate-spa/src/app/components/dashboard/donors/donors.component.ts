import { Component, OnInit } from '@angular/core';
import { BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-donors',
  templateUrl: './donors.component.html',
  styleUrls: ['./donors.component.less']
})
export class DonorsComponent implements OnInit {

  constructor(private modalService: BsModalService) {}

  ngOnInit() {
  }

}
