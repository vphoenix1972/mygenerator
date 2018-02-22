'use strict';

import './usersIndex.css';

import angular from 'angular';
import appModule from 'rootDir/appModule';

function UsersIndexController(connectorService,
    $state,
    dialogService) {
    'ngInject';

    const self = this;

    // Deps
    self._connectorService = connectorService;
    self._$state = $state;
    self._dialogService = dialogService;

    /* Init */
    self.items = [];

    self.isLoading = true;

    self._connectorService.getUsersIndexAsync()
        .then((result) => {
            self.users = result.data;
        },
        () => {
            self._$state.go('admin.home');
        })
        .finally(() => self.isLoading = false);
}

UsersIndexController.prototype.onDeleteButtonClicked = function (user) {
    const self = this;

    self._dialogService.showConfirmAsync({ title: `Are you sure to delete user '${user.name}'?`})
        .then(() => {
            self._dialogService.showExecutingAsync({ title: 'Deleting...' });

            self._connectorService.deleteUserAsync(user.id)
                .then(() => {
                    self.users.remove(user);

                    self._dialogService.showSuccess();
                }, () => {
                    self._dialogService.showErrorAsync({ text: 'An error occured on deletion.' });
                });
        });
}

/* Private */


angular.module(appModule)
    .controller('usersIndexController', UsersIndexController);