'use strict';

import './userSettings.css';

import angular from 'angular';
import appModule from 'rootDir/appModule';

function UserSettingsController(connectorService,
    dialogService) {
    'ngInject';

    const self = this;

    // Deps
    self._connectorService = connectorService;
    self._dialogService = dialogService;

    /* Init */
}

/* Private */


angular.module(appModule)
    .controller('userSettingsController', UserSettingsController);