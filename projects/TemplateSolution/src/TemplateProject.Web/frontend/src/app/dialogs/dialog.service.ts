import { Injectable } from '@angular/core';
import { NgbModal, NgbModalOptions, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { ExecutingDialogComponent } from './executing/executing-dialog.component';
import { ExecutingDialogOptions } from './executing/executing-dialog-options';
import { ConfirmDialogComponent } from './confirm/confirm-dialog.component';
import { ConfirmDialogOptions } from './confirm/confirm-dialog-options';

@Injectable({
    providedIn: 'root'
})
export class DialogService {
    private _executingDialog: NgbModalRef;

    constructor(private _modalService: NgbModal) {

    }

    showSuccess(options: any = null) {
        let defaultOptions = {
            text: 'Operation has completed successfully.'
        };

        if (options != null && typeof options === 'object') {
            if (options.text == null)
                options.text = defaultOptions.text;
        } else {
            options = defaultOptions;
        }

        alert(options.text);
    }

    showError(options: any) {
        alert(options.text);
    }

    showExecuting(options?: ExecutingDialogOptions): void {
        if (options == null)
            options = new ExecutingDialogOptions();

        const modalOptions: NgbModalOptions = {
            backdrop: 'static',
            beforeDismiss: () => this._executingDialog.componentInstance.canCloseByEscAsync()
        };

        this._executingDialog = this._modalService.open(ExecutingDialogComponent, modalOptions);

        this._executingDialog.componentInstance.title = options.title;
        this._executingDialog.componentInstance.onCancelAsync = options.onCancelAsync;
    }

    hideExecuting(): void {
        if (this._executingDialog == null)
            return;

        this._executingDialog.close();

        this._executingDialog = null;
    }

    showConfirmAsync(options?: ConfirmDialogOptions): Promise<void> {
        if (options == null)
            options = new ConfirmDialogOptions();

        const modalOptions: NgbModalOptions = {
            backdrop: 'static'
        };

        const dialog = this._modalService.open(ConfirmDialogComponent, modalOptions);

        dialog.componentInstance.title = options.title;
        dialog.componentInstance.yesButtonText = options.yesButtonText;
        dialog.componentInstance.noButtonText = options.noButtonText;

        return dialog.result;
    }
}
