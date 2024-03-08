import { Component } from '@angular/core';
import { UserAuthService } from './services/user-auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'auth-ng';
  constructor(private accountService: UserAuthService, private router: Router,) {}
  
  ngOnInit(): void {
    this.loadCurrentUser();
  }

  loadCurrentUser() {
    const token = localStorage.getItem('token');
    if(token){
      this.accountService.loadCurrentUser(token).subscribe();
    }else{
      this.router.navigateByUrl('');
    }
  }
}
