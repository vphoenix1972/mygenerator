import { Component, OnInit } from '@angular/core';
import { DialogService } from 'src/app/shared/dialogs/dialog.service';
import { ConfirmResult } from 'src/app/shared/dialogs/confirm/confirm-result';

@Component({
    selector: 'app-examples',
    templateUrl: './examples.component.html',
    styleUrls: ['./examples.component.scss']
})
export class ExamplesComponent implements OnInit {

    constructor(private _dialogService: DialogService) {

    }

    ngOnInit() {
    }

    onShowExecutingButtonClicked(): void {
        this._dialogService.showExecuting({
            onCancelAsync: () => {
                return this._dialogService.showConfirmAsync()
                    .then(result => result == ConfirmResult.Yes);
            }
        });
    }

    onShowSuccessButtonClicked(): void {
        this._dialogService.showSuccess();
    }

    onShowErrorButtonClicked(): void {
        this._dialogService.showErrorAsync()
            .then(() => console.log('Error dialog has been closed!'));
    }
}
