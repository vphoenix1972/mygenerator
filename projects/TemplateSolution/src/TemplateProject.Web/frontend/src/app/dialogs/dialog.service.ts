import { Injectable } from '@angular/core';

import { NgbModal, NgbModalOptions, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { Toast, ToasterService } from 'angular2-toaster';

import { SuccessOptions } from './success/success-options';
import { ErrorDialogComponent } from './error/error-dialog.component';
import { ErrorDialogOptions } from './error/error-dialog-options';
import { ExecutingDialogComponent } from './executing/executing-dialog.component';
import { ExecutingDialogOptions } from './executing/executing-dialog-options';
import { ConfirmDialogComponent } from './confirm/confirm-dialog.component';
import { ConfirmDialogOptions } from './confirm/confirm-dialog-options';

@Injectable({
    providedIn: 'root'
})
export class DialogService {
    private _executingDialog: NgbModalRef;

    constructor(private _modalService: NgbModal,
        private _toasterService: ToasterService
    ) {

    }

    showSuccess(optionsPartial?: Partial<SuccessOptions>) {
        this.hideExecuting();

        const options = new SuccessOptions(optionsPartial);

        const toast: Toast = {
            type: 'success',
            title: options.title,
            body: options.text,
            timeout: 3000
        };

        this._toasterService.pop(toast);
    }

    showErrorAsync(optionsPartial?: Partial<ErrorDialogOptions>): Promise<void> {
        this.hideExecuting();

        const options = new ErrorDialogOptions(optionsPartial);

        let dialog: NgbModalRef;

        const modalOptions: NgbModalOptions = {
            backdrop: 'static',
            beforeDismiss: () => {
                // Return 'resolved' promise if closed by 'ESC'

                dialog.close();

                return false;
            }
        };

        dialog = this._modalService.open(ErrorDialogComponent, modalOptions);

        dialog.componentInstance.title = options.title;
        dialog.componentInstance.text = options.text;

        return dialog.result;
    }

    showExecuting(optionsPartial?: Partial<ExecutingDialogOptions>): void {
        const options = new ExecutingDialogOptions(optionsPartial);

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

    showConfirmAsync(optionsPartial?: Partial<ConfirmDialogOptions>): Promise<void> {
        const options = new ConfirmDialogOptions(optionsPartial);

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
