import { Component, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { UserAuthService } from '../../services/user-auth.service';
import { FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {

  //@ViewChild('registerForm') registerForm: NgForm;
  registerForm: FormGroup;

  isSubmitting: boolean = false;

  constructor(
    private router: Router,
    private userAuthService: UserAuthService,
    private toastr: ToastrService) {
    this.userAuthService = userAuthService;
  }

  ngOnInit(): void {

    this.registerForm = new FormGroup({
      name: new FormControl('', Validators.required),
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', Validators.required),
      confirmPassword: new FormControl('', Validators.required)
    });

  }

  registerAction() {

    console.log(this.registerForm);
    if (this.registerForm.invalid) {
      return;
    }
    this.isSubmitting = true;
    let payload = {
      name: this.registerForm.value.name,
      email: this.registerForm.value.email,
      password: this.registerForm.value.password
    }
    this.userAuthService.register(payload).subscribe({
      next: response => {
        this.router.navigate(['/']);
        this.toastr.success("Registration was success.")
      },
      error: err => {
        debugger
        this.toastr.error(`Form submission failed. If the error persist, contact the administrator.`);
        this.isSubmitting = false;
      },
      complete: () => {
        debugger
        //When arrive here, this will automatically unsubscribe from the service
      }
    });
  }
}
