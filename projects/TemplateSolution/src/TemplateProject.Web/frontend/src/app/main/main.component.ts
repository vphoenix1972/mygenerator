import { Component } from '@angular/core';

import { DialogService } from 'src/app/shared/dialogs/dialog.service';
import { AuthService } from 'src/app/auth/services/auth.service';
import { ConfirmResult } from 'src/app/shared/dialogs/confirm/confirm-result';

@Component({
    selector: 'main-component',
    templateUrl: './main.component.html',
    styleUrls: ['./main.component.scss']
})
export class MainComponent {
    isNavbarNavCollapsed = true;

    constructor(private _authService: AuthService, private _dialogService: DialogService) {

    }

    get username(): string {
        return this._authService.currentUser.name;
    }

    onSignOutClicked(): boolean {

        this._dialogService.showConfirmAsync({ title: 'Are you sure to sign out?' })
            .then(result => {
                if (result != ConfirmResult.Yes)
                    return;

                this._dialogService.showExecuting();

                this._authService.signOutAsync()
                    .then(
                        () => console.log('Goto signIn'),
                        () => this._dialogService.showErrorAsync({ title: 'Sign out error' })
                    )
                    .finally(() => this._dialogService.hideExecuting());
            });

        return false;
    }
}
