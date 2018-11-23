import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, empty } from 'rxjs';
import { tap, catchError, map } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';

import { isString, isArray } from 'src/utils/type-utils';
import { LocalStorageService } from 'src/app/shared/services/storage/localStorageService';

import { User } from 'src/app/auth/models/user';
import { Role } from 'src/app/auth/models/role';
import { JwtClaimTypes } from 'src/app/auth/models/jwtClaimTypes';

const localStorageRefreshTokenKey: string = 'authService.refreshToken';

const paths = {
    signIn: '/security/signin',
    signOut: '/security/signout',
    register: '/security/register',
    refreshToken: '/security/refreshToken',
    changePassword: '/app/user/changePassword'
};

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    private _currentUser: User;
    private _accessToken: string;
    private _refreshToken: string;

    constructor(private _http: HttpClient,
        private _jwt: JwtHelperService,
        private _localStorageService: LocalStorageService) {
        this._currentUser = this.createUnauthenticatedUser();
    }

    get currentUser(): User {
        return new User(this._currentUser);
    }

    get accessToken(): string {
        return this._accessToken;
    }

    loadUserFromCacheAsync(): Promise<void> {
        const refreshToken = this._localStorageService.getValue<string>(localStorageRefreshTokenKey);

        if (isString(refreshToken)) {
            return this._http.post(paths.refreshToken, { refreshToken: refreshToken })
                .pipe(
                    tap(serverData => this.onSignIn(serverData)),
                    catchError(() => {
                        this.onSignedOut();

                        return empty();
                    }),
                    map(() => { }) // Make Observable<void>
                )
                .toPromise();
        }

        return Promise.resolve();
    }

    async signInAsync(login: string, password: string): Promise<void> {

        const data = await this._http.post(paths.signIn, {
            login: login,
            password: password
        }).toPromise();

        this.onSignIn(data);
    }

    async signOutAsync(): Promise<void> {

        if (this.isSignedOut)
            return Promise.resolve();

        await this._http.post(paths.signOut, { refreshToken: this._refreshToken })
            .toPromise();

        return this.onSignedOut();
    }

    async changePasswordAsync(request: { oldPassword: string, newPassword: string}): Promise<void> {
        if (this.isSignedOut)
            return Promise.reject('Current user is not authenticated');

        await this._http.post(paths.changePassword, request)
            .toPromise();
    }

    async registerAsync(ticket: { name: string, email: string, password: string }): Promise<void> {
        const data = await this._http.post(paths.register, ticket)
            .toPromise();

        this.onSignIn(data);
    }

    refreshTokens(): Observable<any> {
        return this._http.post(paths.refreshToken, { refreshToken: this._refreshToken })
            .pipe(
                tap(serverData => this.onSignIn(serverData))
            );
    }

    private get isSignedOut(): boolean {
        return this._refreshToken == null;
    }

    private onSignIn(serverData: any): void {
        this._accessToken = serverData.accessToken;

        const accessTokenData = this._jwt.decodeToken(this._accessToken);

        this.loadUserFromAccessToken(accessTokenData);

        this._refreshToken = serverData.refreshToken;

        this._localStorageService.setValue(localStorageRefreshTokenKey, this._refreshToken);
    }

    private onSignedOut(): void {
        this._currentUser = this.createUnauthenticatedUser();

        this._accessToken = null;

        this._refreshToken = null;

        this._localStorageService.removeValue(localStorageRefreshTokenKey);
    }

    private loadUserFromAccessToken(accessTokenData: any): void {
        this._currentUser = new User({
            isAuthenticated: true,
            id: parseInt(accessTokenData[JwtClaimTypes.UserId]),
            name: accessTokenData[JwtClaimTypes.Name],
            roles: this.parseJwtRole(accessTokenData[JwtClaimTypes.Role])
        });
    }

    private parseJwtRole(jwtRole: string | string[]): Role[] {

        if (isString(jwtRole)) {
            const parseResult = this.parseServerRole(jwtRole as string);
            if (parseResult.isSuccess)
                return [parseResult.value];
        }

        if (isArray(jwtRole)) {
            const result: Role[] = [];

            for (const jwtRoleStr of jwtRole as string[]) {
                if (!isString(jwtRoleStr))
                    continue;

                const parseResult = this.parseServerRole(jwtRoleStr as string);
                if (!parseResult.isSuccess)
                    continue;

                result.push(parseResult.value);
            }

            return result;
        }

        return [];
    }

    private parseServerRole(serverRole: string): { isSuccess: boolean, value: Role } {
        const enumKey = Object.keys(Role).find(key => Role[key] === serverRole);

        if (enumKey == null)
            return { isSuccess: false, value: null };

        return { isSuccess: true, value: Role[enumKey] };
    }

    private createUnauthenticatedUser(): User {
        return new User({
            isAuthenticated: false,
            roles: null
        });
    }
}
