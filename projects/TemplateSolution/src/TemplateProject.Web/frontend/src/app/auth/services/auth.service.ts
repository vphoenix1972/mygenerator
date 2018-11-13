import { Injectable } from '@angular/core';

import { User } from 'src/app/auth/models/user';
import { Roles } from 'src/app/auth/models/roles';

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    private _currentUser: User;

    constructor() {
        this._currentUser = this._createUnauthenticatedUser();
    }

    get currentUser(): User {
        return new User(this._currentUser);
    }

    loadUserFromCacheAsync(): Promise<void> {
        const promise = new Promise<void>((resolve, reject) => {
            setTimeout(() => {
                this._currentUser = new User({ isAuthenticated: true, name: "User", roles: [Roles.User] })

                resolve();
            }, 500);
        });

        return promise;
    }

    signOutAsync(): Promise<void> {
        const promise = new Promise<void>((resolve, reject) => {
            setTimeout(() => {
                this._currentUser = this._createUnauthenticatedUser();

                resolve();
            }, 500);
        });

        return promise;
    }

    _createUnauthenticatedUser(): User {
        return new User({
            isAuthenticated: false,
            roles: null
        });
    }
}
