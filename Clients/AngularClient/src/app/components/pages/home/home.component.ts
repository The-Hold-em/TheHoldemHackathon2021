import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import store from 'src/app/services/identity/store';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  isLogin: boolean = false;
  constructor(private router: Router) {
    this.router.events.subscribe((val) => {
      this.isLogin = store.get('userInfo') ? true : false;
    }
    );
  }

  ngOnInit(): void {
  }
}
