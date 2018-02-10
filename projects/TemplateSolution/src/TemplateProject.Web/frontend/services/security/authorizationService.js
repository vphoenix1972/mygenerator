'use strict';

import angular from 'angular';
import appModule from 'rootDir/appModule';

function AuthorizationService(
    $q,
    $http,
    $injector,
    connectorService,
    jwtService,
    jwtClaimTypes,
    localStorage,
    roles) {

    'ngInject';

    const self = this;

    // Deps
    self._$q = $q;
    self._$http = $http;
    self._$injector = $injector;
    self._connectorService = connectorService;
    self._jwtService = jwtService;
    self._jwtClaimTypes = jwtClaimTypes;
    self._localStorage = localStorage;
    self._roles = roles;

    // Init
    self._localStorageAccessTokenKey = 'authorizationService.accessToken';
    self._currentUser = self._createUnauthorizedUser();
}

AuthorizationService.prototype.currentUser = function () {
    const self = this;

    return angular.copy(self._currentUser);
}

AuthorizationService.prototype.signInAsync = function (options) {
    const self = this;

    if (!angular.isObject(options))
        throw new Error('Parameter "options" must be an object');

    return self._connectorService.signInAsync(options)
        .then((response) => {
            self._onSignIn(response.data.accessToken);
        });
}

AuthorizationService.prototype.signOutAsync = function () {
    const self = this;

    self._onSignOut();

    return self._$q.resolve();
}

AuthorizationService.prototype.registerAsync = function (ticket) {
    const self = this;

    return self._connectorService.registerAsync(ticket)
        .then((response) => {
            self._onSignIn(response.data.accessToken);
        });
}

AuthorizationService.prototype.loadUserFromCacheAsync = function () {
    const self = this;

    var accessToken = self._localStorage.getItem(self._localStorageAccessTokenKey);
    if (angular.isString(accessToken)) {
        return self._connectorService.validateToken({ accessToken: accessToken })
            .then(() => {
                    self._onSignIn(accessToken);
                },
                () => {
                    self._onSignOut();

                    return self._$q.resolve();
                });
    }

    self._onSignOut();

    return self._$q.resolve();
}

/* Private */

AuthorizationService.prototype._onSignIn = function (accessToken) {
    const self = this;

    self._loadUserFromAccessToken(accessToken);
    self._connectorService.setAccessToken(accessToken);

    self._localStorage.setItem(self._localStorageAccessTokenKey, accessToken);
}

AuthorizationService.prototype._onSignOut = function () {
    const self = this;

    self._currentUser = self._createUnauthorizedUser();

    self._connectorService.setAccessToken(null);

    self._localStorage.removeItem(self._localStorageAccessTokenKey);
}

AuthorizationService.prototype._createUnauthorizedUser = function () {
    const self = this;

    return self._createUser();
}

AuthorizationService.prototype._createUser = function (options) {
    var user = {
        isAuthenticated: false,
        roles: null
    }

    angular.extend(user, options);

    return user;
}

AuthorizationService.prototype._loadUserFromAccessToken = function (accessToken) {
    const self = this;

    var payload = self._jwtService.decodeToken(accessToken);

    self._currentUser.isAuthenticated = true;
    self._currentUser.name = payload[self._jwtClaimTypes.name];
    self._currentUser.roles = self._parseJwtRole(payload[self._jwtClaimTypes.role]);
}

AuthorizationService.prototype._parseJwtRole = function (jwtRole) {
    if (angular.isArray(jwtRole))
        return jwtRole;

    if (angular.isString(jwtRole))
        return [jwtRole];

    return [];
}


angular.module(appModule)
    .service('authorizationService', AuthorizationService);