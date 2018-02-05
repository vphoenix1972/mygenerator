﻿'use strict';

import angular from 'angular';
import appModule from 'rootDir/appModule';

function AuthorizationService(
    $q,
    $http,
    $injector,
    localStorage,
    roles) {

    'ngInject';

    const self = this;

    // Deps
    self._$q = $q;
    self._$http = $http;
    self._$injector = $injector;
    self._localStorage = localStorage;
    self._roles = roles;

    // Init
    self._localStorageUserKey = 'authorizationService.user';
    self._currentUser = self._createUnauthorizedUser();
    self._users = [
        {
            email: 'admin@gmail.com',
            name: 'admin',
            password: '1234',
            roles: [self._roles.user, self._roles.admin]
        },
        {
            email: 'user@gmail.com',
            name: 'user',
            password: '1',
            roles: [self._roles.user]
        }
    ];
}

AuthorizationService.prototype.currentUser = function () {
    const self = this;

    return angular.copy(self._currentUser);
}

AuthorizationService.prototype.signInAsync = function (options) {
    const self = this;

    if (!angular.isObject(options))
        throw new Error('Parameter "options" must be an object');

    var user = self._users.find(e => (e.email === options.login || e.name === options.login) &&
        e.password === options.password);
    if (user == null)
        return self._$q.reject();

    self._currentUser.isAuthenticated = true;
    self._currentUser.roles = angular.copy(user.roles);

    self._localStorage.setItem(self._localStorageUserKey, self._currentUser);

    return self._$q.resolve();
}

AuthorizationService.prototype.signOutAsync = function () {
    const self = this;

    self._currentUser = self._createUnauthorizedUser();

    self._localStorage.removeItem(self._localStorageUserKey);

    return self._$q.resolve();
}

AuthorizationService.prototype.registerAsync = function (ticket) {
    const self = this;

    var newUser = {
        email: ticket.email,
        name: ticket.username,
        password: ticket.password,
        roles: [self._roles.user]
    };

    self._users.push(newUser);

    self._currentUser.isAuthenticated = true;
    self._currentUser.roles = angular.copy(newUser.roles);

    self._localStorage.setItem(self._localStorageUserKey, self._currentUser);

    return self._$q.resolve();
}

AuthorizationService.prototype.loadUserFromCacheAsync = function () {
    const self = this;

    var userFromStorage = self._localStorage.getItem(self._localStorageUserKey);
    if (angular.isObject(userFromStorage)) {
        self._currentUser = userFromStorage;
    }

    return self._$q.resolve();
}

/* Private */

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