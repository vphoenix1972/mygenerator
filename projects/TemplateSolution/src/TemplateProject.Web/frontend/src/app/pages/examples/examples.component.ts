import { Component, OnInit } from '@angular/core';
import { DialogService } from 'src/app/dialogs/dialog.service';
import { ExecutingDialogOptions } from 'src/app/dialogs/executing/executing-dialog-options';

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
        this._dialogService.showExecuting(new ExecutingDialogOptions({
            onCancelAsync: () => this._dialogService.showConfirmAsync()
        }));
    }

    onShowSuccessButtonClicked(): void {
        console.log('onShowSuccessButtonClicked');
    }

    onShowErrorButtonClicked(): void {
        console.log('onShowErrorButtonClicked');
    }
}
