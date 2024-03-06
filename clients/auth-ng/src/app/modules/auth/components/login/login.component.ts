import { Component, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  @ViewChild('loginForm') loginForm: NgForm;
  
  isSubmitting: boolean = false;
  passwordHidden = true;

  login() {
    if (this.loginForm.invalid) {
      return;
    } 
  }

  togglePasswordVisibility() {
    this.passwordHidden = !this.passwordHidden;
  }

}
