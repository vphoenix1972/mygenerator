import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ToasterModule } from 'angular2-toaster';

import { EntryPointComponent } from './entry-point.component';
import { SharedModule } from './shared/shared.module';
import { MainModule } from './main/main.module';
import { EntryPointRoutingModule } from './entry-point-routing.module';

@NgModule({
    declarations: [
        EntryPointComponent
    ],
    imports: [
        BrowserModule,
        NgbModule.forRoot(),
        ToasterModule.forRoot(),
        SharedModule,
        MainModule,
        EntryPointRoutingModule
    ],
    providers: [],
    bootstrap: [EntryPointComponent],
    entryComponents: [
    ]
})
export class EntryPointModule { }
