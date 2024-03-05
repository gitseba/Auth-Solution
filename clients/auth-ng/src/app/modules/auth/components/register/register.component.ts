import { Component, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { UserAuthService } from '../../services/user-auth.service';
import { Form, FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  
  @ViewChild('registerForm') registerForm: NgForm;
  isSubmitting: boolean = false;
  
  constructor(
    private router: Router,
    private userAuthService: UserAuthService, 
    private toastr: ToastrService) {
    this.userAuthService = userAuthService;
  }
 
  ngOnInit(): void {
 
  }
 
  registerAction() {

    if(this.registerForm.invalid){
      return;
    }
    this.isSubmitting = true;
    let payload = {
      name:this.registerForm.value.name,
      email:this.registerForm.value.email,
      password:this.registerForm.value.password
    }
    this.userAuthService
    .register(payload).subscribe({
      next: response =>{ 
        console.log(response); 
        this.router.navigate(['/']);
        this.toastr.success("Registration was success.")
      },
      error: error => {
        // Handle error response
        console.error('Form submission failed:', error);
        // Display error message to the user
        this.toastr.error(`Form submission failed: ${error.message}`);
        // Clear loading state if applicable
        // Re-enable submit button
        setTimeout(() => {
          this.isSubmitting = false;  
        }, 4000);
      },
      complete: () => {
        console.log("Registration request is completed.")
        //When arrive here, this will automatically unsubscribe from the service
        this.isSubmitting = false;
      }
    });
  }
}
