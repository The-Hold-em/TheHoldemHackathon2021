import { Component, OnInit } from '@angular/core';
import IdentityService from 'src/app/services/identity/identity.service';
const identityService = new IdentityService();

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

  async login(idnumber: string, pass: string) {
    var data = {
      username: idnumber,
      password: pass
    }
    var result = await identityService.signInAsync(data);
    await identityService.getUserInfoAsync();
    console.log(result);
  }
}
