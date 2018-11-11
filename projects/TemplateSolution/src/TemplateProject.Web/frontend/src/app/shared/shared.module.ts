import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { ExecutingDialogComponent } from './dialogs/executing/executing-dialog.component';
import { ConfirmDialogComponent } from './dialogs/confirm/confirm-dialog.component';
import { ErrorDialogComponent } from './dialogs/error/error-dialog.component';

import { SpinnerComponent } from './components/spinner/spinner.component';
import { ErrorIconComponent } from './components/error-icon/error-icon.component';
import { PanelPageCenteredComponent } from './components/panel-page-centered/panel-page-centered.component';

@NgModule({
    declarations: [
        ExecutingDialogComponent,
        ConfirmDialogComponent,
        ErrorDialogComponent,
        SpinnerComponent,
        ErrorIconComponent,
        PanelPageCenteredComponent
    ],
    imports: [
        BrowserAnimationsModule
    ],
    exports: [
        SpinnerComponent,
        ErrorIconComponent,
        PanelPageCenteredComponent
    ],
    entryComponents: [
        ExecutingDialogComponent,
        ConfirmDialogComponent,
        ErrorDialogComponent,
    ]
})
export class SharedModule { }
