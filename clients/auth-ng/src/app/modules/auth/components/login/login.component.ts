import { Component, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { UserAuthService } from '../../services/user-auth.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  @ViewChild('loginForm') loginForm: NgForm;
  
  isSubmitting: boolean = false;
  passwordHidden = true;

  constructor(private router: Router,
    private userAuthService: UserAuthService,
    private toastr: ToastrService){
  }

  login() {
    if (this.loginForm.invalid) {
      return;
    }
    this.isSubmitting = true;
    let payload = {
      email: this.loginForm.value.email,
      password: this.loginForm.value.password
    }
    this.userAuthService.login(payload).subscribe({
      next: response => {
        this.router.navigate(['/']);
        this.toastr.success("Login was success.")
      },
      error: err => {
        debugger
        this.toastr.error(`Form submission failed. If the error persist, contact the administrator.`);
        this.isSubmitting = false;
      },
      complete: () => { 
        this.isSubmitting = false;
        //When arrive here, this will automatically unsubscribe from the service
      }
    });
  }

  togglePasswordVisibility() {
    this.passwordHidden = !this.passwordHidden;
  }

}
