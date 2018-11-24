import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { DialogService } from 'src/app/shared/dialogs/dialog.service';
import { AuthService } from 'src/app/auth/services/auth.service';
import { Role } from 'src/app/auth/models/role';
import { ConfirmResult } from 'src/app/shared/dialogs/confirm/confirm-result';

@Component({
    selector: 'main-component',
    templateUrl: './main.component.html',
    styleUrls: ['./main.component.scss']
})
export class MainComponent {
    isNavbarNavCollapsed: boolean = true;
    username: string;
    isGotoAdminLinkVisible: boolean;

    constructor(private _authService: AuthService,
        private _dialogService: DialogService,
        private _router: Router) {

        this._authService.currentUser.subscribe(currentUser => {
            if (!currentUser.isAuthenticated)
                return;

            this.username = currentUser.name;
            this.isGotoAdminLinkVisible = currentUser.roles.any(role => role == Role.Admin);
        });
    }

    onSignOutClicked(): boolean {

        this._dialogService.showConfirmAsync({ title: 'Are you sure to sign out?' })
            .then(result => {
                if (result != ConfirmResult.Yes)
                    return;

                this._dialogService.showExecuting();

                this._authService.signOutAsync()
                    .then(
                        () => this._router.navigate(['/sign-in']),
                        () => this._dialogService.showErrorAsync({ title: 'Sign out error' })
                    )
                    .finally(() => this._dialogService.hideExecuting());
            });

        return false;
    }
}
