import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS } from '@angular/common/http';

import { JwtModule } from '@auth0/angular-jwt';
import { JwtHelperService } from '@auth0/angular-jwt';
import { JWT_OPTIONS } from '@auth0/angular-jwt';

import { SharedModule } from 'src/app/shared/shared.module';
import { AuthRoutingModule } from './auth-routing.module';

import { AuthInterceptor } from './interceptors/auth.interceptor';

import { ForbiddenComponent } from './pages/forbidden/forbidden.component';
import { SignInComponent } from './pages/sign-in/sign-in.component';
import { RegisterComponent } from './pages/register/register.component';

import { PasswordChangeComponent } from './components/password-change/password-change.component';

@NgModule({
    declarations: [
        ForbiddenComponent,
        SignInComponent,
        RegisterComponent,
        PasswordChangeComponent
    ],
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        JwtModule,
        SharedModule,
        AuthRoutingModule
    ],
    exports: [
        PasswordChangeComponent
    ],
    entryComponents: [

    ],
    providers: [
        {
          provide: HTTP_INTERCEPTORS,
          useClass: AuthInterceptor,
          multi: true
        },
        JwtHelperService,
        {
            provide: JWT_OPTIONS,
            useValue: null
        }
      ]
})
export class AuthModule { }