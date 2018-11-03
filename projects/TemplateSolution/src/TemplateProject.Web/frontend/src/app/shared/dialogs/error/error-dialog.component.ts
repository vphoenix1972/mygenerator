import { Component, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
    templateUrl: './error-dialog.component.html',
    styleUrls: ['./error-dialog.component.scss']
})
export class ErrorDialogComponent {
    @Input() title: string;
    @Input() text: string;

    constructor(private _modalInstance: NgbActiveModal) {

    }

    onCloseButtonClicked(): void {
        this._modalInstance.close();
    }
}
