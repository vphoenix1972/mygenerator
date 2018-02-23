'use strict';

import './adminLayout.css';

import angular from 'angular';
import appModule from 'rootDir/appModule';

function AdminLayoutController($state,
    dialogService,
    authorizationService) {
    'ngInject';

    const self = this;

    // Deps
    self._$state = $state;
    self._dialogService = dialogService;
    self._authorizationService = authorizationService;

    // Init
    self.username = self._authorizationService.currentUser().name;
}

AdminLayoutController.prototype.onSignOutClicked = function () {
    var self = this;

    self._dialogService.showConfirmAsync({
        title: 'Are you sure to sign out?'
    }).then(() => {
        self._dialogService.showExecutingAsync();

        self._authorizationService.signOutAsync()
            .then(
                () => self._$state.go('signIn'),
                () => self._dialogService.showErrorAsync({ title: 'Sign out error' })
            )
            .finally(() => self._dialogService.hideExecuting());
    });
}

/* Private */

angular.module(appModule)
    .controller('adminLayoutController', AdminLayoutController);