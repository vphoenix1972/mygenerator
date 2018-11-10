import { Injectable } from '@angular/core';
import { CanActivate, CanActivateChild, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';

import { AuthModule } from 'src/app/auth/auth.module';
import { Roles } from 'src/app/auth/models/roles';
import { AuthService } from 'src/app/auth/services/auth.service';

@Injectable({
    providedIn: AuthModule
})
export class AuthGuard implements CanActivate, CanActivateChild {

    constructor(private _router: Router, private _authService: AuthService) {

    }

    canActivate(route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot): boolean {

        const roles = route.data.roles as Roles[];

        return this.checkAuthorized(roles)
    }

    canActivateChild(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot): boolean {

        const roles = this.findRolesAscendingPathTree(route);

        return this.checkAuthorized(roles);
    }

    private findRolesAscendingPathTree(route: ActivatedRouteSnapshot): Roles[]
    {
        const roles = route.data.roles as Roles[];
        if (roles != null)
            return roles;

        if (route.parent != null)
            return this.findRolesAscendingPathTree(route.parent);

        return null;
    }

    private checkAuthorized(roles: Roles[]): boolean {
        const allowAnonymous = roles == null || roles.length < 1;

        if (allowAnonymous)
            return true;

        const user = this._authService.currentUser;

        if (!user.isAuthenticated) {
            console.log('signIn');
            return false;
        }

        const isAccessAllowed = roles.all(
            requiredRole => user.roles.any(role => role === requiredRole)
        );

        if (!isAccessAllowed) {
            console.log('forbidden');
            return false;
        }

        console.log('allowed');

        return true;
    }
}