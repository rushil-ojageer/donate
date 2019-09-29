import { Component, OnInit } from '@angular/core';
import { faHandsHelping, faSignOutAlt } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.less']
})
export class AppComponent implements OnInit {
  title = 'donate-spa';
  isAuthenticated = true;
  faHandsHelping = faHandsHelping;
  faSignOutAlt = faSignOutAlt;

  constructor() { }

  ngOnInit(): void {
  }
}
