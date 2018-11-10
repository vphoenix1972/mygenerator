import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AuthGuard } from 'src/app/auth/guards/auth.guard';
import { Roles } from 'src/app/auth/models/roles';

import { MainComponent } from './main.component';
import { HomeComponent } from './pages/home/home.component';
import { ExamplesComponent } from './pages/examples/examples.component';
import { AboutComponent } from './pages/about/about.component';
import { TodoIndexComponent } from './pages/todo/index/todo-index.component';
import { TodoEditComponent } from './pages/todo/edit/todo-edit.component';

const routes: Routes = [

    {
        path: 'main',
        component: MainComponent,
        canActivateChild: [AuthGuard],
        data: { roles: [Roles.User] },
        children: [
            { path: 'home', component: HomeComponent },
            { path: 'examples', component: ExamplesComponent },
            { path: 'about', component: AboutComponent },
            { path: 'todo/index', component: TodoIndexComponent },
            { path: 'todo/new', component: TodoEditComponent },
            { path: 'todo/edit/:id', component: TodoEditComponent },

            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: '**', redirectTo: 'home' }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class MainRoutingModule { }
