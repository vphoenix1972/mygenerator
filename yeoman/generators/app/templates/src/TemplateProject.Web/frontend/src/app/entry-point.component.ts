import { Component } from '@angular/core';
import { Router } from "@angular/router";
import { Location } from "@angular/common";

import { IconType } from 'src/app/shared/components/panel-page-centered/icon-type';

import { ToasterConfig } from 'angular2-toaster';

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

    constructor(private _router: Router, private _location: Location) {
        const path = this._location.path(true);

        this.isLoading = true;

        setTimeout(() => {
            this._router.navigateByUrl(path);

            this.isLoading = false;
        }, 500);
    }
}