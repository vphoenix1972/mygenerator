'use strict';

import './todoEdit.css';

import angular from 'angular';
import appModule from 'rootDir/appModule';

function TodoEditController(connectorService,
    $state,
    dialogService,
    $stateParams) {
    'ngInject';

    const self = this;

    // Deps
    self._connectorService = connectorService;
    self._$state = $state;
    self._dialogService = dialogService;

    /* Init */
    self.id = $stateParams.id;
    self.isNew = self.id == null;
    self.isLoading = false;
    self.name = '';
        
    if (!self.isNew){
        self.isLoading = true;

        self._connectorService.getTodoEditAsync(self.id)
            .then((result) => {
                self._fromServerModel(result.data);
            },
            () => {
                self._$state.go('app.home');
            })
            .finally(() => self.isLoading = false);
    }
}

TodoEditController.prototype.onBackToIndexButtonClicked = function () {
    const self = this;

    self._$state.go('app.todo-index');
}

TodoEditController.prototype.onSaveButtonClicked = function () {
    const self = this;

    var serverModel = self._toServerModel();

    var savePromise = self.isNew ?
        self._connectorService.addTodoItemAsync(serverModel) :
        self._connectorService.updateTodoItemAsync(self.id, serverModel);

    self._dialogService.showExecutingAsync({ title: 'Saving...' });

    savePromise.then(() => {
        self._dialogService.showSuccess();

        self._$state.go('app.todo-index');
    }, () => {
        self._dialogService.showErrorAsync({ text: 'An error occured on saving.' });
    });
}

/* Private */

TodoEditController.prototype._fromServerModel = function (serverModel) {
    const self = this;

    self.name = serverModel.name;
}

TodoEditController.prototype._toServerModel = function () {
    const self = this;

    return {
        id: self.isNew ? null : self.id,
        name: self.name
    };
}


angular.module(appModule)
    .controller('todoEditController', TodoEditController);