import { Component, OnInit, OnChanges, SimpleChanges } from '@angular/core';
import store from 'src/app/services/identity/store';
import IdentityService from 'src/app/services/identity/identity.service';
import { Router } from '@angular/router';
const identityService = new IdentityService();

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})
export class NavComponent implements OnInit {

  isLogin: boolean = false;
  user: any;
  constructor(private router: Router) {
    this.router.events.subscribe((val) => {
      this.isLogin = store.get('userInfo') ? true : false;
      this.user = store.get('userInfo');
    }
    );
  }

  ngOnInit(): void {
  }

  async logOut() {
    await identityService.signOutAsync();
  }
}
