import { BrowserModule } from '@angular/platform-browser';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './/app-routing.module';
import { HomeComponent } from './home/home.component';
import { ExamplesComponent } from './examples/examples.component';
import { AboutComponent } from './about/about.component';
import { TodoIndexComponent } from './todo-index/todo-index.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    ExamplesComponent,
    AboutComponent,
    TodoIndexComponent
  ],
  imports: [
    BrowserModule,
    NgbModule.forRoot(),
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
