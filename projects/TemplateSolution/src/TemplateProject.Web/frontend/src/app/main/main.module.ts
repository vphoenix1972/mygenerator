import { NgModule } from '@angular/core';

import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { SharedModule } from 'src/app/shared/shared.module';
import { AuthModule } from 'src/app/auth/auth.module';
import { MainRoutingModule } from './main-routing.module';

import { MainComponent } from './main.component';
import { HomeComponent } from './pages/home/home.component';
import { ExamplesComponent } from './pages/examples/examples.component';
import { AboutComponent } from './pages/about/about.component';
import { TodoIndexComponent } from './pages/todo/index/todo-index.component';
import { TodoEditComponent } from './pages/todo/edit/todo-edit.component';

@NgModule({
    declarations: [
        MainComponent,
        HomeComponent,
        ExamplesComponent,
        AboutComponent,
        TodoIndexComponent,
        TodoEditComponent
    ],
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        HttpClientModule,
        NgbModule,
        SharedModule,
        AuthModule,
        MainRoutingModule
    ],
    exports: [

    ],
    entryComponents: [

    ]
})
export class MainModule { }
