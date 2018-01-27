'use strict';

import './todoIndex.css';

import angular from 'angular';
import appModule from 'rootDir/appModule';

function TodoIndexController(connectorService,
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

    self._connectorService.getTodoIndexAsync()
        .then((result) => {
            self.items = result.data;
        },
        () => {
            self._$state.go('home');
        })
        .finally(() => self.isLoading = false);
}

TodoIndexController.prototype.onAddButtonClicked = function () {
    const self = this;
    
    self._$state.go('todo-new');
}

TodoIndexController.prototype.onEditButtonClicked = function (item) {
    const self = this;

    self._$state.go('todo-edit', { id: item.id });
}

TodoIndexController.prototype.onDeleteButtonClicked = function (item) {
    const self = this;

    self._dialogService.showConfirmAsync({ title: `Are you sure to delete item '${item.name}'?`})
        .then(() => {
            self._dialogService.showExecutingAsync({ title: 'Deleting...' });

            self._connectorService.deleteTodoItemAsync(item.id)
                .then(() => {
                    self.items.remove(item);

                    self._dialogService.showSuccess();
                }, () => {
                    self._dialogService.showErrorAsync({ text: 'An error occured on deletion.' });
                });
        });
}

/* Private */


angular.module(appModule)
    .controller('todoIndexController', TodoIndexController);