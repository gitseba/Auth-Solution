import { AbstractControl, FormControl, ValidationErrors } from "@angular/forms"

export class CustomValidators {

    static noSpaceAllowed(control: FormControl) {
        if (control.value !== null && control.value.indexOf(' ') !== -1) {
            return { noSpaceAllowed: true }
        }
        return null;
    }

    static passwordRequirements(control: AbstractControl): ValidationErrors | null {

        const passwordControl = control.parent?.get('password'); // Retrieve the password control from the parent form group
        if (!passwordControl) {
          return null;
        }
        const passw = passwordControl?.value!;
        const errors: ValidationErrors = {};

        // Check if the string is 3 to 20 characters long
        const isLengthValid = passw.length >= 3 && passw.length <= 20;
        errors['length-req'] = !isLengthValid;
    
        // // Check for at least one digit
        // const hasDigit = /\d/.test(passw);
        // errors['digit'] = !hasDigit;
    
        // // Check for at least one lowercase letter
        // const hasLowercase = /[a-z]/.test(passw);
        // errors['lowerCase-letter'] = !hasLowercase;
    
        // // Check for at least one uppercase letter
        // const hasUppercase = /[A-Z]/.test(passw);
        // errors['upperCase-letter'] = !hasUppercase;
    
        // // Check for at least one special character
        // const hasSpecialChar = /[!@#&()–{}:;',?*~$^+=<>]/.test(passw);
        // errors['special'] = !hasSpecialChar;
    
        let result =  Object.values(errors).includes(true);
        return result ? errors : null;
      }
}