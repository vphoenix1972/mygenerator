'use strict';

import './confirmDialog.css';

import angular from 'angular';
import appModule from 'rootDir/appModule';

function ConfirmDialogController(
    $uibModalInstance,
    options) {
    'ngInject';

    const self = this;

    // Deps
    self._$uibModalInstance = $uibModalInstance;

    // Store options passed into dialog
    self.options = options;    
}

ConfirmDialogController.prototype.onYesButtonClicked = function () {
    const self = this;

    self._$uibModalInstance.close();
}

ConfirmDialogController.prototype.onNoButtonClicked = function () {
    const self = this;

    self._$uibModalInstance.dismiss();
}

/* Private */


angular.module(appModule)
    .controller('confirmDialogController', ConfirmDialogController);