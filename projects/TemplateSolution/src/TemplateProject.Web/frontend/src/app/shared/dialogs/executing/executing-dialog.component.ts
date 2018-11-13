import { Component, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
    templateUrl: './executing-dialog.component.html',
    styleUrls: ['./executing-dialog.component.scss']
})
export class ExecutingDialogComponent {
    private _isCancelling: boolean = false;

    @Input() title: string;
    @Input() onCancelAsync: () => Promise<boolean>;

    constructor(private _modalInstance: NgbActiveModal) {

    }

    canCloseByEscAsync(): Promise<boolean> {
        // Executing dialog must be closed with resolved promise,
        // but 'ESC' key causes the dialog to be closed by reject,
        // so forbid the closing and close dialog via cancel function

        if (this.isCancellationAllowed()) {
            this.cancel();
        }

        return Promise.resolve(false);
    }

    isCancelButtonVisible(): boolean {
        return this.isCancellationAllowed();
    }

    onCancelButtonClicked(): void {
        this.cancel();
    }

    private isCancellationAllowed(): boolean {
        return this.onCancelAsync != null;
    }

    private cancel(): void {
        if (this._isCancelling)
            return;

        this._isCancelling = true;

        this.onCancelAsync()
            .then(isCancelled => {
                if (isCancelled)
                    this._modalInstance.close();
            })
            .finally(() => this._isCancelling = false);
    }
}
