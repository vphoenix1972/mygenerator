'use strict';

import './appLayout.css';

import angular from 'angular';
import appModule from 'rootDir/appModule';

function AppLayoutController($state,
    dialogService,
    authorizationService) {
    'ngInject';

    const self = this;

    // Deps
    self._$state = $state;
    self._dialogService = dialogService;
    self._authorizationService = authorizationService;
}

AppLayoutController.prototype.onSignOutClicked = function () {
    var self = this;

    self._dialogService.showConfirmAsync({
        title: 'Are you sure to sign out?'
    }).then(() => {
        self._dialogService.showExecutingAsync();

        self._authorizationService.signOutAsync()
            .then(() => self._$state.go('signIn'))
            .finally(() => self._dialogService.hideExecuting());
    });
}

/* Private */

angular.module(appModule)
    .controller('appLayoutController', AppLayoutController);