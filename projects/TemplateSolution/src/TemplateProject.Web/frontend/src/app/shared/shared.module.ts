import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { ExecutingDialogComponent } from './dialogs/executing/executing-dialog.component';
import { ConfirmDialogComponent } from './dialogs/confirm/confirm-dialog.component';
import { ErrorDialogComponent } from './dialogs/error/error-dialog.component';

import { SpinnerComponent } from './components/spinner/spinner.component';
import { PanelPageCenteredComponent } from './components/panel-page-centered/panel-page-centered.component';

@NgModule({
    declarations: [
        ExecutingDialogComponent,
        ConfirmDialogComponent,
        ErrorDialogComponent,
        SpinnerComponent,
        PanelPageCenteredComponent
    ],
    imports: [
        BrowserAnimationsModule
    ],
    exports: [
        SpinnerComponent,
        PanelPageCenteredComponent
    ],
    entryComponents: [
        ExecutingDialogComponent,
        ConfirmDialogComponent,
        ErrorDialogComponent,
    ]
})
export class SharedModule { }
