import { Injectable } from '@angular/core';

import { User } from 'src/app/auth/models/user';
import { Roles } from 'src/app/auth/models/roles';

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    private _currentUser: User;

    constructor() {
        this._currentUser = new User({
            isAuthenticated: false,
            roles: null
        })
    }

    get currentUser(): User {
        return new User(this._currentUser);
    }

    loadUserFromCacheAsync(): Promise<void> {
        const promise = new Promise<void>((resolve, reject) => {
            setTimeout(() => {
                this._currentUser = new User({ isAuthenticated: true, roles: [Roles.User] })

                resolve();
            }, 500);
        });

        return promise;
    }
}
