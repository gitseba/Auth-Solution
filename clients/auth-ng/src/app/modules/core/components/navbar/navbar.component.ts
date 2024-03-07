import { Component } from '@angular/core';
import { UserAuthService } from 'src/app/modules/auth/services/user-auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {

  constructor(public accountService: UserAuthService){

  }
}
