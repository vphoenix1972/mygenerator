import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { ExecutingDialogComponent } from './dialogs/executing/executing-dialog.component';
import { ConfirmDialogComponent } from './dialogs/confirm/confirm-dialog.component';
import { ErrorDialogComponent } from './dialogs/error/error-dialog.component';

import { SpinnerComponent } from './components/spinner/spinner.component';
import { PanelPageCenteredComponent } from './components/panel-page-centered/panel-page-centered.component';
import { OverlayComponent } from './components/overlay/overlay.component';

import { SortableHeaderDirective } from './directives/sortable-header/sortable-header.directive';

@NgModule({
    declarations: [
        ExecutingDialogComponent,
        ConfirmDialogComponent,
        ErrorDialogComponent,
        SpinnerComponent,
        PanelPageCenteredComponent,
        OverlayComponent,
        SortableHeaderDirective
    ],
    imports: [
        BrowserAnimationsModule
    ],
    exports: [
        SpinnerComponent,
        PanelPageCenteredComponent,
        OverlayComponent,
        SortableHeaderDirective
    ],
    entryComponents: [
        ExecutingDialogComponent,
        ConfirmDialogComponent,
        ErrorDialogComponent,
    ]
})
export class SharedModule { }
