'use strict';

import './dialogService.css';

import progressDialogTemplateUrl from './progressDialog/progressDialog.tpl.html';
import confirmDialogTemplateUrl from './confirmDialog/confirmDialog.tpl.html';
import errorDialogTemplateUrl from './errorDialog/errorDialog.tpl.html';

import angular from 'angular';
import appModule from 'rootDir/appModule';

function DialogService(
    $uibModal,
    toaster) {
    'ngInject';

    const self = this;

    // Deps
    self._$uibModal = $uibModal;
    self._toaster = toaster;

    self._executingDialog = null;
}

DialogService.prototype.showSuccess = function (options) {
    var self = this;

    var toasterOptions = {
        type: 'success',
        title: 'Success',
        body: 'Operation has completed successfully.',
        showCloseButton: true
    };

    angular.merge(toasterOptions, options);

    self.hideExecuting();

    self.showToaster(toasterOptions);
}

DialogService.prototype.showExecutingAsync = function (options) {
    const self = this;

    var defaultOptions = {
        controllerName: 'progressDialogController',
        title: 'Executing...',
        onCancelAsync: null
    }
    var currentOptions = angular.extend({}, defaultOptions, options);

    // Show mapping window
    var modalOptions = {
        backdrop: 'static',
        keyboard: false,
        templateUrl: progressDialogTemplateUrl,
        controller: currentOptions.controllerName,
        controllerAs: 'vm',
        resolve: {
            options: function () {
                return currentOptions;
            }
        }
    }

    self._executingDialog = {
        instance: null,
        isHideRequested: false,
        isOpened: false
    }

    self._executingDialog.instance = self.showModal(modalOptions);

    self._executingDialog.instance.opened.then(() => {
        self._executingDialog.isOpened = true;

        // Workaround to allow to hide executing right after it showExecuting has been called
        if (self._executingDialog.isHideRequested)
            self._executingDialog.instance.close();
    });

    self._executingDialog.instance.closed.then(() => {
        self._executingDialog = null;
    });

    return self._executingDialog.instance.result;
}

DialogService.prototype.hideExecuting = function () {
    const self = this;

    if (self._executingDialog == null)
        return;

    self._executingDialog.isHideRequested = true;

    if (self._executingDialog.isOpened)
        self._executingDialog.instance.close();
}

DialogService.prototype.isExecutingShown = function () {
    const self = this;

    return self._executingDialog != null;
}

DialogService.prototype.showErrorAsync = function (options) {
    const self = this;

    var defaultOptions = {
        controllerName: 'errorDialogController',
        title: 'Error',
        text: 'A error occurred.'
    }
    var currentOptions = angular.extend({}, defaultOptions, options);

    // Show mapping window
    var modalOptions = {
        backdrop: 'static',
        keyboard: true,
        templateUrl: errorDialogTemplateUrl,
        controller: currentOptions.controllerName,
        controllerAs: 'vm',
        resolve: {
            options: function () {
                return currentOptions;
            }
        }
    }

    self.hideExecuting();

    var instance = self.showModal(modalOptions);

    return instance.result;
}

DialogService.prototype.showConfirmAsync = function (options) {
    const self = this;

    var defaultOptions = {
        controllerName: 'confirmDialogController',
        title: 'Are you sure?',
        yesButtonText: 'Yes',
        noButtonText: 'No'
    }
    var currentOptions = angular.extend({}, defaultOptions, options);

    // Show mapping window
    var modalOptions = {
        backdrop: 'static',
        keyboard: true,
        templateUrl: confirmDialogTemplateUrl,
        controller: currentOptions.controllerName,
        controllerAs: 'vm',
        resolve: {
            options: function () {
                return currentOptions;
            }
        }
    }

    var instance = self.showModal(modalOptions);

    return instance.result;
}

DialogService.prototype.showToaster = function (options) {
    var self = this;

    var toasterOptions = {
    };

    angular.merge(toasterOptions, options);

    self._toaster.pop(toasterOptions);
}

DialogService.prototype.showModal = function (modalOptions) {
    const self = this;

    var modalInstance = self._$uibModal.open(modalOptions);

    return modalInstance;
}

/* Private */


angular.module(appModule)
    .service('dialogService', DialogService);