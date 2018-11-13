import { Component, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ConfirmResult } from './confirm-result';

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

    closeByEsc() : boolean {
        // Dialog must be closed with resolved promise,
        // but 'ESC' key causes the dialog to be closed by reject,
        // so forbid the closing and close dialog via cancel function

        this._modalInstance.close(ConfirmResult.No);

        return false;
    }

    onYesButtonClicked(): void {
        this._modalInstance.close(ConfirmResult.Yes);
    }

    onNoButtonClicked(): void {
        this._modalInstance.dismiss(ConfirmResult.No);
    }
}
