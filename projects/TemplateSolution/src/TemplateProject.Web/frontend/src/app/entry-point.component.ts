import { Component } from '@angular/core';
import { ToasterConfig } from 'angular2-toaster';

@Component({
    selector: 'entry-point',
    templateUrl: './entry-point.component.html',
    styleUrls: ['./entry-point.component.scss']
})
export class EntryPointComponent {
    isNavbarNavCollapsed = true;
    toasterConfig: ToasterConfig = new ToasterConfig({ positionClass: 'toast-bottom-right' });
}
