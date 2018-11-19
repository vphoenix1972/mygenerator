import { Injectable } from '@angular/core';
import {
    HttpRequest,
    HttpHandler,
    HttpEvent,
    HttpInterceptor
} from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable, Subject } from 'rxjs';
import { catchError, switchMap } from 'rxjs/operators'

import { AuthService } from 'src/app/auth/services/auth.service';
import { DialogService } from 'src/app/shared/dialogs/dialog.service';

@Injectable()
export class SetAccessTokenInterceptor implements HttpInterceptor {
    private _isTokenRefreshInProgress: boolean = false;
    private _tokenRefreshingSubject: Subject<any>;

    constructor(public _authService: AuthService,
        private _dialogService: DialogService,
        private _router: Router) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

        const isSignedIn = this._authService.currentUser.isAuthenticated;

        if (isSignedIn) {
            request = this.addAuthHeader(request);

            return next.handle(request).pipe(
                catchError(error => {

                    if (error.status === 401) {
                        return this.refreshToken()
                            .pipe(
                                switchMap(() => {
                                    request = this.addAuthHeader(request);

                                    return next.handle(request);
                                })
                            );
                    }

                    return Observable.throw(error);
                }));
        }

        return next.handle(request);
    }

    private get isTokenRefreshInProgress(): boolean {
        return this._tokenRefreshingSubject != null;
    }

    private addAuthHeader(request: HttpRequest<any>): HttpRequest<any> {
        return request.clone({
            setHeaders: {
                Authorization: `Bearer ${this._authService.accessToken}`
            }
        });
    }

    private refreshToken(): Subject<any> {
        if (!this.isTokenRefreshInProgress) {
            this._tokenRefreshingSubject = new Subject<any>();

            this._authService.refreshTokens()
                .subscribe(() => {
                    this._tokenRefreshingSubject.next();

                    this._tokenRefreshingSubject.complete();

                    this._tokenRefreshingSubject = null;
                }, () => {
                    this._authService.signOutAsync()
                        .finally(() => {
                            this._tokenRefreshingSubject.complete();

                            this._tokenRefreshingSubject = null;

                            this._dialogService.showErrorAsync({ title: 'Sign out', text: 'You have been signed out' })
                                .finally(() => {
                                    this._router.navigate(['/sign-in']);
                                });
                        });
                });
        }

        return this._tokenRefreshingSubject;
    }
}