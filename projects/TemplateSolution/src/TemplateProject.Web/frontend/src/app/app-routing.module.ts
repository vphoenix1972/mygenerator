import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { HomeComponent } from './pages/home/home.component';
import { TodoIndexComponent } from './pages/todo/index/todo-index.component';
import { ExamplesComponent } from './pages/examples/examples.component';
import { AboutComponent } from './pages/about/about.component';

const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'todo/index', component: TodoIndexComponent },
  { path: 'examples', component: ExamplesComponent },
  { path: 'about', component: AboutComponent },

  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: '**', redirectTo: '/home' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
