import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';

import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ToasterModule } from 'angular2-toaster';

import { EntryPointComponent } from './entry-point.component';
import { SharedModule } from './shared/shared.module';
import { AppRoutingModule } from './app-routing.module';

import { HomeComponent } from './pages/home/home.component';
import { ExamplesComponent } from './pages/examples/examples.component';
import { AboutComponent } from './pages/about/about.component';
import { TodoIndexComponent } from './pages/todo/index/todo-index.component';
import { TodoEditComponent } from './pages/todo/edit/todo-edit.component';

@NgModule({
    declarations: [
        EntryPointComponent,
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
        ToasterModule.forRoot(),
        SharedModule,
        AppRoutingModule
    ],
    providers: [],
    bootstrap: [EntryPointComponent],
    entryComponents: [
    ]
})
export class EntryPointModule { }
