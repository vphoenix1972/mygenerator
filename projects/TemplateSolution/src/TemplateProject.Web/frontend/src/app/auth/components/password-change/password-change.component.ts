import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators, AbstractControl, ValidationErrors } from '@angular/forms';

import { AuthService } from 'src/app/auth/services/auth.service';
import { DialogService } from 'src/app/shared/dialogs/dialog.service';

@Component({
    selector: 'app-password-change',
    templateUrl: './password-change.component.html',
    styleUrls: ['./password-change.component.scss']
})
export class PasswordChangeComponent {
    oldPasswordControl: FormControl;
    newPasswordControl: FormControl;
    confirmNewPasswordControl: FormControl;
    passwordChangeForm: FormGroup;

    constructor(private _dialogService: DialogService,
        private _authService: AuthService) {

        this.oldPasswordControl = new FormControl('', [Validators.required]);
        this.newPasswordControl = new FormControl('', [Validators.required, Validators.minLength(4)]);
        this.confirmNewPasswordControl = new FormControl('', [
            (control: AbstractControl): ValidationErrors | null => {
                if (this.newPasswordControl.value === '' && control.value === '') {
                    return {
                        confirmPassword: 'Password is required.'
                    }
                }

                if (control.value !== this.newPasswordControl.value) {
                    return {
                        confirmPassword: 'Passwords are not equal.'
                    }
                }

                return null;
            }
        ]);

        this.newPasswordControl.valueChanges.subscribe(() => {
            this.confirmNewPasswordControl.markAsTouched();
            this.confirmNewPasswordControl.updateValueAndValidity();
        });

        this.passwordChangeForm = new FormGroup({
            oldPassword: this.oldPasswordControl,
            newPassword: this.newPasswordControl,
            confirmNewPassword: this.confirmNewPasswordControl,
        });
    }

    async onSubmit() {
        this._dialogService.showExecuting({ title: 'Changing password...' });

        const request = {
            oldPassword: this.oldPasswordControl.value,
            newPassword: this.newPasswordControl.value
        };

        try {
            await this._authService.changePasswordAsync(request);
        }
        catch {
            this._dialogService.showErrorAsync({
                title: 'Change password error',
                text: 'A error occured during password change. Please try again later.'
            });

            return;
        }
        finally {
            this._dialogService.hideExecuting();
        }

        this._dialogService.showSuccess();
    }
}