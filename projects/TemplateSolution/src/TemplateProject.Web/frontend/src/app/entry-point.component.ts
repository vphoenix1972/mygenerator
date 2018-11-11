import { Component } from '@angular/core';
import { Router } from "@angular/router";
import { Location } from "@angular/common";

import { ToasterConfig } from 'angular2-toaster';

import { IconType } from 'src/app/shared/components/panel-page-centered/icon-type';

import { AuthService } from 'src/app/auth/services/auth.service';

@Component({
    selector: 'entry-point',
    templateUrl: './entry-point.component.html',
    styleUrls: ['./entry-point.component.scss']
})
export class EntryPointComponent {
    IconType = IconType;

    isLoading: boolean = true;
    isError: boolean = false;
    toasterConfig: ToasterConfig = new ToasterConfig({ positionClass: 'toast-bottom-right' });

    constructor(private _router: Router,
        private _location: Location,
        private _authService: AuthService) {
        const path = this._location.path(true);

        // Startup logic

        this.isLoading = true;

        this._authService.loadUserFromCacheAsync()
            .then(() => {
                this._router.navigateByUrl(path);
            }, () => {
                this.isError = true;
            })
            .finally(() => this.isLoading = false);
    }
}