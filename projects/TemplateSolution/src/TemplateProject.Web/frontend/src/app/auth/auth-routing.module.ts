import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ForbiddenComponent } from './pages/forbidden/forbidden.component';
import { SignInComponent } from './pages/sign-in/sign-in.component';

const routes: Routes = [

    {
        path: 'forbidden',
        component: ForbiddenComponent
    },
    {
        path: 'sign-in',
        component: SignInComponent
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class AuthRoutingModule { }
