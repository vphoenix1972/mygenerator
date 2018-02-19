'use strict';

import angular from 'angular';
import appModule from 'rootDir/appModule';

function AuthorizationService(
    $q,
    $http,
    $injector,
    $state,
    connectorService,
    dialogService,
    refreshTokenTimerFactory,
    jwtService,
    jwtClaimTypes,
    localStorage,
    roles,
    securityConstants) {

    'ngInject';

    const self = this;

    // Deps
    self._$q = $q;
    self._$http = $http;
    self._$injector = $injector;
    self._$state = $state;
    self._connectorService = connectorService;
    self._dialogService = dialogService;
    self._jwtService = jwtService;
    self._jwtClaimTypes = jwtClaimTypes;
    self._localStorage = localStorage;
    self._roles = roles;

    // Init
    self._localStorageRefreshTokenKey = securityConstants.localStorageRefreshTokenKey;

    self._currentUser = self._createUnauthorizedUser();

    self._refreshTokenTimer = refreshTokenTimerFactory.create();
    self._refreshTokenTimer.onTokensRenewed = (tokens) => self._onTokensRenewed(tokens);
    self._refreshTokenTimer.onError = () => self._onTokensRenewalError();
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
            self._onSignIn(response.data);
        });
}

AuthorizationService.prototype.signOutAsync = function () {
    const self = this;

    if (self._isSignedOut())
        return self._$q.resolve();

    return self._connectorService.signOutAsync({ refreshToken: self._refreshToken })
        .then(() => self._onSignOut());
}

AuthorizationService.prototype.registerAsync = function (ticket) {
    const self = this;

    return self._connectorService.registerAsync(ticket)
        .then((response) => {
            self._onSignIn(response.data);
        });
}

AuthorizationService.prototype.loadUserFromCacheAsync = function () {
    const self = this;

    var refreshToken = self._localStorage.getItem(self._localStorageRefreshTokenKey);
    if (angular.isString(refreshToken)) {
        return self._connectorService.refreshToken({ refreshToken: refreshToken })
            .then((response) => {
                    self._onSignIn(response.data);
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

AuthorizationService.prototype._onSignIn = function (signInModel) {
    const self = this;

    self._refreshToken = signInModel.refreshToken;

    var accessTokenData = self._jwtService.decodeToken(signInModel.accessToken);

    self._loadUserFromAccessToken(accessTokenData);

    self._connectorService.setAccessToken(signInModel.accessToken);

    self._localStorage.setItem(self._localStorageRefreshTokenKey, self._refreshToken);

    self._refreshTokenTimer.start({ accessTokenData: accessTokenData, refreshToken: self._refreshToken });
}

AuthorizationService.prototype._onSignOut = function () {
    const self = this;

    self._currentUser = self._createUnauthorizedUser();

    self._connectorService.setAccessToken(null);

    self._localStorage.removeItem(self._localStorageRefreshTokenKey);
    self._refreshTokenTimer.stop();

    self._refreshToken = null;
}

AuthorizationService.prototype._onTokensRenewed = function (tokens) {
    const self = this;

    self._onSignIn(tokens);
}

AuthorizationService.prototype._onTokensRenewalError = function () {
    const self = this;

    self._onSignOut();

    self._dialogService.showErrorAsync({ title: 'Sign out', text: 'You have been signed out' })
        .finally(() => self._$state.go('signIn'));
}

AuthorizationService.prototype._loadUserFromAccessToken = function (accessTokenData) {
    const self = this;

    self._currentUser.isAuthenticated = true;
    self._currentUser.name = accessTokenData[self._jwtClaimTypes.name];
    self._currentUser.roles = self._parseJwtRole(accessTokenData[self._jwtClaimTypes.role]);
}

AuthorizationService.prototype._parseJwtRole = function (jwtRole) {
    if (angular.isArray(jwtRole))
        return jwtRole;

    if (angular.isString(jwtRole))
        return [jwtRole];

    return [];
}

AuthorizationService.prototype._isSignedOut = function () {
    const self = this;

    return self._refreshToken == null;
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


angular.module(appModule)
    .service('authorizationService', AuthorizationService);