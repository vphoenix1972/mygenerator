import { NgModule } from '@angular/core';

import { SharedModule } from 'src/app/shared/shared.module';
import { AuthRoutingModule } from './auth-routing.module';

import { ForbiddenComponent } from './pages/forbidden/forbidden.component';

@NgModule({
    declarations: [
        ForbiddenComponent
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