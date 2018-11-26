import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormGroup, FormControl, Validators, AbstractControl, ValidationErrors } from '@angular/forms';

import { AuthService } from 'src/app/auth/services/auth.service';
import { DialogService } from 'src/app/shared/dialogs/dialog.service';
import { asModelState } from 'src/app/shared/model-state/model-state';

@Component({
    selector: 'register-component',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
    emailControl: FormControl;
    userNameControl: FormControl;
    passwordControl: FormControl;
    confirmPasswordControl: FormControl;
    registerForm: FormGroup;

    constructor(private _dialogService: DialogService,
        private _authService: AuthService,
        private _router: Router) {

        this.emailControl = new FormControl('', [Validators.required, Validators.email]);
        this.userNameControl = new FormControl('', [Validators.required]);
        this.passwordControl = new FormControl('', [Validators.required, Validators.minLength(4)]);
        this.confirmPasswordControl = new FormControl('', [
            (control: AbstractControl): ValidationErrors | null => {
                if (this.passwordControl.value === '' && control.value === '') {
                    return {
                        confirmPassword: 'Password is required.'
                    }
                }

                if (control.value !== this.passwordControl.value) {
                    return {
                        confirmPassword: 'Passwords are not equal.'
                    }
                }

                return null;
            }
        ]);

        this.passwordControl.valueChanges.subscribe(() => {
            this.confirmPasswordControl.markAsTouched();
            this.confirmPasswordControl.updateValueAndValidity();
        });

        this.registerForm = new FormGroup({
            email: this.emailControl,
            userName: this.userNameControl,
            password: this.passwordControl,
            confirmPassword: this.confirmPasswordControl,
        });
    }

    async onSubmit() {
        this._dialogService.showExecuting({ title: 'Registration...' });

        const ticket = {
            email: this.emailControl.value,
            name: this.userNameControl.value,
            password: this.passwordControl.value
        };

        try {
            await this._authService.registerAsync(ticket);
        }
        catch (error) {
            let errorText = 'A error occured during password change. Please try again later.';

            const modelState = asModelState(error);
            if (modelState != null)
                errorText = modelState.convertToString();

            this._dialogService.showErrorAsync({
                title: 'Registration error',
                text: errorText
            });

            return;
        }
        finally {
            this._dialogService.hideExecuting();
        }

        this._router.navigate(['/main/home']);
    }
}
