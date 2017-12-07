'use strict';

import angular from 'angular';
import appModule from 'rootDir/appModule';

function TodoIndexController(connectorService,
    $state) {
    'ngInject';

    const self = this;

    self._connectorService = connectorService;
    self._$state = $state;

    /* Init */
    self.items = [];

    self.isLoading = true;

    self._connectorService.getTodoItemsAsync()
        .then((result) => {
            self.items = result.data;
        },
        () => {
            self._$state.go('home');
        })
        .finally(() => self.isLoading = false);
}

/* Private */


angular.module(appModule)
    .controller('todoIndexController', TodoIndexController);