import { Component } from '@angular/core';

import { IconType } from 'src/app/shared/components/panel-page-centered/icon-type';

@Component({
    selector: 'forbidden-component',
    templateUrl: './forbidden.component.html',
    styleUrls: ['./forbidden.component.scss']
})
export class ForbiddenComponent {
    IconType = IconType;
}
