import { Component, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
    templateUrl: './confirm-dialog.component.html',
    styleUrls: ['./confirm-dialog.component.scss']
})
export class ConfirmDialogComponent {

    @Input() title: string;
    @Input() yesButtonText: string;
    @Input() noButtonText: string;

    constructor(private _modalInstance: NgbActiveModal) {

    }

    onYesButtonClicked(): void {
        this._modalInstance.close();
    }

    onNoButtonClicked(): void {
        this._modalInstance.dismiss();
    }
}
