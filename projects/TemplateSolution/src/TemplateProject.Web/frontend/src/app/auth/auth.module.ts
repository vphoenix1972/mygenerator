import { NgModule } from '@angular/core';

import { SharedModule } from 'src/app/shared/shared.module';
import { AuthRoutingModule } from './auth-routing.module';

import { ForbiddenComponent } from './pages/forbidden/forbidden.component';
import { SignInComponent } from './pages/sign-in/sign-in.component';

@NgModule({
    declarations: [
        ForbiddenComponent,
        SignInComponent
    ],
    imports: [
        SharedModule,
        AuthRoutingModule
    ],
    exports: [

    ],
    entryComponents: [

    ]
})
export class AuthModule { }