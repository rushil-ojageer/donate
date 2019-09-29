import { Component, OnInit } from '@angular/core';
import { faRibbon, faDonate } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.less']
})
export class DashboardComponent implements OnInit {

  faRibbon = faRibbon;
  faDonate = faDonate;

  constructor() { }

  ngOnInit() {
  }

}
