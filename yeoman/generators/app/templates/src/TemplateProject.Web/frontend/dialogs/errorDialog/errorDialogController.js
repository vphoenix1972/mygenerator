'use strict';

import './errorDialog.css';

import angular from 'angular';
import appModule from 'rootDir/appModule';

function ErrorDialogController(
    $uibModalInstance,
    options) {
    'ngInject';

    const self = this;

    // Deps
    self._$uibModalInstance = $uibModalInstance;

    // Store options passed into dialog
    self.options = options;    
}

ErrorDialogController.prototype.onCloseButtonClicked = function () {
    const self = this;

    self._$uibModalInstance.dismiss();
}

/* Private */


angular.module(appModule)
    .controller('errorDialogController', ErrorDialogController);