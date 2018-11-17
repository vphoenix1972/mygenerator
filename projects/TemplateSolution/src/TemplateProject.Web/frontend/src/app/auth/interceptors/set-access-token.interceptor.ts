import { Injectable } from '@angular/core';
import {
    HttpRequest,
    HttpHandler,
    HttpEvent,
    HttpInterceptor
} from '@angular/common/http';
import { AuthService } from 'src/app/auth/services/auth.service';
import { Observable } from 'rxjs';

@Injectable()
export class SetAccessTokenInterceptor implements HttpInterceptor {
    constructor(public auth: AuthService) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

        const accessToken = this.auth.accessToken;

        if (accessToken != null) {
            request = request.clone({
                setHeaders: {
                    Authorization: `Bearer ${this.auth.accessToken}`
                }
            });
        }

        return next.handle(request);
    }
}