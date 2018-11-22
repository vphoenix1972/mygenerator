import { NgModule } from '@angular/core';

import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { SharedModule } from 'src/app/shared/shared.module';
import { AuthModule } from 'src/app/auth/auth.module';
import { AdminRoutingModule } from './admin-routing.module';

import { AdminComponent } from './admin.component';
import { HomeComponent } from './pages/home/home.component';
import { UsersIndexComponent } from './pages/users/users-index.component';

@NgModule({
    declarations: [
        AdminComponent,
        HomeComponent,
        UsersIndexComponent
    ],
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        HttpClientModule,
        NgbModule,
        SharedModule,
        AuthModule,
        AdminRoutingModule
    ],
    exports: [

    ],
    entryComponents: [

    ]
})
export class AdminModule { }
