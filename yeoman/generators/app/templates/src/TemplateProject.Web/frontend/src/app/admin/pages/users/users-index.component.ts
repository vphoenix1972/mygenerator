import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';

import { DialogService } from 'src/app/shared/dialogs/dialog.service';
import { ConfirmResult } from 'src/app/shared/dialogs/confirm/confirm-result';

@Component({
    selector: 'admin-users-index',
    templateUrl: './users-index.component.html',
    styleUrls: ['./users-index.component.scss']
})
export class UsersIndexComponent implements OnInit {
    isLoading: boolean;
    users: any;

    constructor(private _http: HttpClient,
        private _dialogService: DialogService,
        private _router: Router) { }

    ngOnInit() {
        this.isLoading = true;

        this._http.get('/admin/users/index')
            .subscribe(serverUsers => {
                this.users = serverUsers;

                this.isLoading = false;
            }, error => {
                this._dialogService.showErrorAsync({ title: 'Error', text: 'A error occured while getting a list of users. Please try again later.' })
                    .then(() => this._router.navigate(['/admin']));
            });
    }

    onDeleteButtonClicked(user: any): boolean {
        this._dialogService.showConfirmAsync({ title: `Are you sure to delete user '${user.name}'?` })
            .then(result => {
                if (result != ConfirmResult.Yes)
                    return;

                this._dialogService.showExecuting();

                this._http.delete(`/admin/users/${user.id}`)
                    .subscribe(() => {
                        this.users.splice(this.users.indexOf(user), 1);

                        this._dialogService.showSuccess();
                    }, () => this._dialogService.showErrorAsync({ title: 'Error', text: 'A error occured while deleting a user. Please try again later.' }));
            });

        return false;
    }
}
