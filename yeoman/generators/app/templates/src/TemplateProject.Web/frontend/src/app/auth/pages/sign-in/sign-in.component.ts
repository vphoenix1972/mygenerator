import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormControl } from '@angular/forms';

import { AuthService } from 'src/app/auth/services/auth.service';
import { DialogService } from 'src/app/shared/dialogs/dialog.service';

@Component({
    selector: 'sign-in-component',
    templateUrl: './sign-in.component.html',
    styleUrls: ['./sign-in.component.scss']
})
export class SignInComponent {
    loginControl: FormControl;
    passwordControl: FormControl;
    signInForm: FormGroup;

    private _returnUrl: string;

    constructor(private _authService: AuthService,
        private _dialogService: DialogService,
        private _router: Router,
        route: ActivatedRoute) {

        route.queryParams.subscribe(params => this._returnUrl = params['returnUrl'] || '/');

        this.loginControl = new FormControl('');
        this.passwordControl = new FormControl('');

        this.signInForm = new FormGroup({
            login: this.loginControl,
            password: this.passwordControl
        });
    }

    onSubmit(): void {
        this._dialogService.showExecuting({ title: 'Signing in...' });

        this._authService.signInAsync(this.signInForm.value.login, this.signInForm.value.password)
            .then(
                () => {
                    this._router.navigateByUrl(this._returnUrl);
                },
                () => this._dialogService.showErrorAsync({ title: 'Sign in error', text: 'Incorrect username or password' })
            )
            .finally(() => this._dialogService.hideExecuting());
    }
}
