import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import IdentityService from 'src/app/services/identity/identity.service';
const identityService = new IdentityService();

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  constructor(private router: Router) { }

  ngOnInit(): void {
  }

  async login(idnumber: string, pass: string) {
    var data = {
      username: idnumber,
      password: pass
    }
    var result = await identityService.signInAsync(data);
    await identityService.getUserInfoAsync();
    if (result.isSuccessful) {
      this.router.navigate(['elections']);
    }
    console.log(result);

  }
}
