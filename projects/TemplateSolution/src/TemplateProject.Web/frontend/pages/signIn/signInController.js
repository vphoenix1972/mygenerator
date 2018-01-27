'use strict';

import './signIn.css';

import angular from 'angular';
import appModule from 'rootDir/appModule';

function SignInController($state,
    authorizationService,
    dialogService) {
    'ngInject';

    const self = this;

    // Deps
    self._$state = $state;
    self._authorizationService = authorizationService;
    self._dialogService = dialogService;

    // Init
    self.isAuthError = false;
}

SignInController.prototype.onSignInButtonClicked = function () {
    const self = this;

    self.isAuthError = false;

    self._dialogService.showExecutingAsync({ title: 'Signing in...' });

    var signInOptions = {
        login: self.login,
        password: self.password
    };

    self._authorizationService.signInAsync(signInOptions)
        .then(
            () => self._$state.go('app.home'),
            () => self.isAuthError = true
        )
        .finally(() => self._dialogService.hideExecuting());
}

angular.module(appModule)
    .controller('signInController', SignInController);