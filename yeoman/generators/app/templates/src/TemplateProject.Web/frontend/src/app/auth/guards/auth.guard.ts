import { Injectable } from '@angular/core';
import { CanActivate, CanActivateChild, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';

import { AuthModule } from 'src/app/auth/auth.module';
import { Role } from 'src/app/auth/models/role';
import { AuthService } from 'src/app/auth/services/auth.service';

@Injectable({
    providedIn: AuthModule
})
export class AuthGuard implements CanActivate, CanActivateChild {

    constructor(private _router: Router, private _authService: AuthService) {

    }

    canActivate(route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot): boolean {

        const roles = route.data.roles as Role[];

        return this.authorize(roles, state.url);
    }

    canActivateChild(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot): boolean {

        const roles = this.findRolesAscendingPathTree(route);

        return this.authorize(roles, state.url);
    }

    private findRolesAscendingPathTree(route: ActivatedRouteSnapshot): Role[] {
        const roles = route.data.roles as Role[];
        if (roles != null)
            return roles;

        if (route.parent != null)
            return this.findRolesAscendingPathTree(route.parent);

        return null;
    }

    private authorize(roles: Role[], returnUrl: string): boolean {
        const allowAnonymous = roles == null || roles.length < 1;

        if (allowAnonymous)
            return true;

        const user = this._authService.currentUser;

        if (!user.isAuthenticated) {
            this._router.navigate(['/sign-in'], { queryParams: { returnUrl: returnUrl } });
            return false;
        }

        const isAccessAllowed = roles.all(
            requiredRole => user.roles.any(role => role === requiredRole)
        );

        if (!isAccessAllowed) {
            this._router.navigate(['/forbidden']);
            return false;
        }

        return true;
    }
}