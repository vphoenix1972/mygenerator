'use strict';

import './register.css';

import angular from 'angular';
import appModule from 'rootDir/appModule';

function RegisterController($state,
    authorizationService,
    dialogService) {
    'ngInject';

    const self = this;

    // Deps
    self._$state = $state;
    self._authorizationService = authorizationService;
    self._dialogService = dialogService;

    // Init
    self.registrationError = null;
}

RegisterController.prototype.onRegisterButtonClicked = function () {
    const self = this;

    self.registrationError = null;

    self._validateInput();
    if (self.registrationError)
        return;

    self._dialogService.showExecutingAsync({ title: 'Registration...' });

    var ticket = {
        email: self.email,
        username: self.username,
        password: self.password
    };

    self._authorizationService.registerAsync(ticket)
        .then(
            () => self._$state.go('app.home'),
            (error) => self.registrationError = error
        )
        .finally(() => self._dialogService.hideExecuting());
}

/* Private */

RegisterController.prototype._validateInput = function () {
    const self = this;

    if (String.prototype.isNullOrWhiteSpace(self.email)) {
        self.registrationError = 'Email cannot be empty';
        return;
    }

    if (String.prototype.isNullOrWhiteSpace(self.username)) {
        self.registrationError = 'Username cannot be empty';
        return;
    }

    if (String.prototype.isNullOrWhiteSpace(self.password)) {
        self.registrationError = 'Password cannot be empty';
        return;
    }

    if (self.password !== self.passwordConfirmation) {
        self.registrationError = 'Passwords are not equal';
        return;
    }
}


angular.module(appModule)
    .controller('registerController', RegisterController);