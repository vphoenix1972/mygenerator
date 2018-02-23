'use strict';

import './progressDialog.css';

import templateUrl from './progressDialog.tpl.html';

import angular from 'angular';
import appModule from 'rootDir/appModule';

function ProgressDialogController(
    $uibModalInstance,
    options) {
    'ngInject';

    const self = this;

    // Deps
    self._$uibModalInstance = $uibModalInstance;

    // Store options passed into dialog
    self.options = options;

    // Default dialog options
    self.title = self.options.title;

    self._isCancelling = false;
}

ProgressDialogController.prototype.isCancelButtonVisible = function () {
    const self = this;

    return angular.isFunction(self.options.onCancelAsync);
}

ProgressDialogController.prototype.onCancelButtonClicked = function () {
    const self = this;

    if (self._isCancelling)
        return;

    self._isCancelling = true;

    self.options.onCancelAsync()
        .then(() => self._$uibModalInstance.dismiss())
        .finally(() => self._isCancelling = false);
}

/* Private */


angular.module(appModule)
    .controller('progressDialogController', ProgressDialogController)
    // Preload dialog's template to show dialog even if server is unavailable
    .run([
        '$http', '$templateCache', function ($http, $templateCache) {
            $http.get(templateUrl)
                .then(function (response) {
                    $templateCache.put(templateUrl, response.data);
                });
        }
    ]);