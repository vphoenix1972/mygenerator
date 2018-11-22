import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AuthGuard } from 'src/app/auth/guards/auth.guard';
import { Role } from 'src/app/auth/models/role';

import { AdminComponent } from './admin.component';
import { HomeComponent } from './pages/home/home.component';

const routes: Routes = [

    {
        path: 'admin',
        component: AdminComponent,
        canActivateChild: [AuthGuard],
        data: { roles: [Role.User, Role.Admin] },
        children: [
            { path: 'home', component: HomeComponent },

            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: '**', redirectTo: 'home' }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class AdminRoutingModule { }
