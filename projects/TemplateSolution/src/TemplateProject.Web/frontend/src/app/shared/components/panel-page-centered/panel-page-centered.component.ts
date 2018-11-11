import { Component, OnInit, Input } from '@angular/core';
import { IconType } from './icon-type';

@Component({
    selector: 'app-panel-page-centered',
    templateUrl: './panel-page-centered.component.html',
    styleUrls: ['./panel-page-centered.component.scss']
})
export class PanelPageCenteredComponent implements OnInit {
    @Input() title: string;
    @Input() text: string;
    @Input() iconType: IconType = IconType.Error;

    constructor() { }

    ngOnInit() {
    }

    get iconClass(): string {
        return 'fa-exclamation-circle';
    }

}
