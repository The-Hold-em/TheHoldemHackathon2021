import { Component, OnInit } from '@angular/core';
import IdentityService from 'src/app/services/identity/identity.service';
const identityService = new IdentityService();

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

  async register(name: string, lastname: string, idnumber: string, pass1: string, pass2: string, birthdate: string) {
    var data = {
      FirstName: name,
      LastName: lastname,
      IdentityNumber: idnumber,
      Password: pass1,
      PasswordConfirmation: pass2,
      DateOfBirth: new Date(birthdate)
    }
    await identityService.connectTokenAsync();
    var result = await identityService.signUpAsync(data);
    console.log(result);
  }

}
