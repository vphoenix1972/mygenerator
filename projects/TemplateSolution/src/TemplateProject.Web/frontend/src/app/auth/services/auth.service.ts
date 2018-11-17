import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { JwtHelperService } from '@auth0/angular-jwt';

import { isString, isArray } from 'src/utils/type-utils';

import { User } from 'src/app/auth/models/user';
import { Roles } from 'src/app/auth/models/roles';
import { JwtClaimTypes } from 'src/app/auth/models/jwtClaimTypes';

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    private _currentUser: User;
    private _accessToken: string;

    constructor(private _http: HttpClient, private _jwt: JwtHelperService) {
        this._currentUser = this.createUnauthenticatedUser();
    }

    get currentUser(): User {
        return new User(this._currentUser);
    }

    get accessToken(): string {
        return this._accessToken;
    }

    loadUserFromCacheAsync(): Promise<void> {
        const promise = new Promise<void>((resolve, reject) => {
            setTimeout(() => {
                resolve();
            }, 500);
        });

        return promise;
    }

    async signInAsync(login: string, password: string): Promise<void> {

        const data = await this._http.post('/security/signin', {
            login: login,
            password: password
        }).toPromise();

        this.onSignIn(data);
    }

    async signOutAsync(): Promise<void> {

        if (this.isSignedOut)
            return Promise.resolve();

        await Promise.resolve();

        return this.onSignedOut();
    }

    private get isSignedOut(): boolean {
        return this._accessToken == null;
    }

    private onSignIn(serverData: any): void {
        this._accessToken = serverData.accessToken;

        const accessTokenData = this._jwt.decodeToken(this._accessToken);

        this.loadUserFromAccessToken(accessTokenData);
    }

    private onSignedOut(): void {
        this._currentUser = this.createUnauthenticatedUser();

        this._accessToken = null;
    }

    private loadUserFromAccessToken(accessTokenData: any): void {
        this._currentUser = new User({
            isAuthenticated: true,
            id: parseInt(accessTokenData[JwtClaimTypes.UserId]),
            name: accessTokenData[JwtClaimTypes.Name],
            roles: this.parseJwtRole(accessTokenData[JwtClaimTypes.Role])
        });
    }

    private parseJwtRole(jwtRole: string | string[]): Roles[] {

        if (isString(jwtRole)) {
            const parseResult = this.parseServerRole(jwtRole as string);
            if (parseResult.isSuccess)
                return [parseResult.value];
        }

        if (isArray(jwtRole)) {
            const result: Roles[] = [];

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

    private parseServerRole(serverRole: string): { isSuccess: boolean, value: Roles } {
        const enumKey = Object.keys(Roles).find(key => Roles[key] === serverRole);

        if (enumKey == null)
            return { isSuccess: false, value: null };

        return { isSuccess: true, value: Roles[enumKey] };
    }

    private createUnauthenticatedUser(): User {
        return new User({
            isAuthenticated: false,
            roles: null
        });
    }
}
