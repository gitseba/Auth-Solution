import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { UserAuthService } from '../../../../services/user-auth.service';
import { AbstractControl, AsyncValidatorFn, FormControl, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { CustomValidators } from 'src/app/validators/custom.validator';
import { debounceTime, take, switchMap, map, finalize } from 'rxjs';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  registerForm: FormGroup;
  isSubmitting: boolean = false;
  passwordInFocus: boolean = false;
  passwordHidden = true;

  constructor(
    private router: Router,
    private userAuthService: UserAuthService,
    private toastr: ToastrService) {
  }

  ngOnInit(): void {

    this.registerForm = new FormGroup({
      name: new FormControl('', [Validators.required, CustomValidators.noSpaceAllowed]),
      email: new FormControl('', [Validators.required, Validators.email], [this.validateEmailExistsAlready()]),
      password: new FormControl('',
        [
          Validators.required,
          (c: AbstractControl) => CustomValidators.passwordRequirements(c),
        ],
      ),
      confirmPassword: new FormControl('', Validators.required)
    });

  }

  register() {
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

  validateEmailExistsAlready(): AsyncValidatorFn {
    return (control: AbstractControl) => {
      return control.valueChanges.pipe(
        debounceTime(1000),
        take(1),
        switchMap(() => {
          return this.userAuthService.checkEmailExists(control.value)
            .pipe(
              map(result => {
                return result ? { emailExists: true } : null;
              }),
              finalize(() => control.markAsTouched())
            )
        })
      )
    }
  }
}
