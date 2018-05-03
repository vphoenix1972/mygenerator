'use strict';

import './appLayout.css';

import angular from 'angular';
import appModule from 'rootDir/appModule';

function AppLayoutController($state,
    dialogService) {
    'ngInject';

    const self = this;

    // Deps
    self._$state = $state;
    self._dialogService = dialogService;

    // Init
}


/* Private */

angular.module(appModule)
    .controller('appLayoutController', AppLayoutController);