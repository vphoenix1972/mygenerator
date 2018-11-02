import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './/app-routing.module';

import { SpinnerComponent } from './components/spinner/spinner.component';

import { ExecutingDialogComponent } from './dialogs/executing/executing-dialog.component';
import { ConfirmDialogComponent } from './dialogs/confirm/confirm-dialog.component';

import { HomeComponent } from './pages/home/home.component';
import { ExamplesComponent } from './pages/examples/examples.component';
import { AboutComponent } from './pages/about/about.component';
import { TodoIndexComponent } from './pages/todo/index/todo-index.component';
import { TodoEditComponent } from './pages/todo/edit/todo-edit.component';

@NgModule({
    declarations: [
        AppComponent,
        SpinnerComponent,
        ExecutingDialogComponent,
        ConfirmDialogComponent,
        HomeComponent,
        ExamplesComponent,
        AboutComponent,
        TodoIndexComponent,
        TodoEditComponent
    ],
    imports: [
        BrowserModule,
        FormsModule,
        ReactiveFormsModule,
        HttpClientModule,
        NgbModule.forRoot(),
        AppRoutingModule
    ],
    providers: [],
    bootstrap: [AppComponent],
    entryComponents: [
        ExecutingDialogComponent,
        ConfirmDialogComponent
    ]
})
export class AppModule { }
