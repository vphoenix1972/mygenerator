'use strict';

import './errorDialog.css';

import templateUrl from './errorDialog.tpl.html';

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

    self._$uibModalInstance.close();
}

/* Private */


angular.module(appModule)
    .controller('errorDialogController', ErrorDialogController)
    // Preload dialog's template to show dialog even if server is unavailable
    .run([
        '$http', '$templateCache', function ($http, $templateCache) {
            $http.get(templateUrl)
                .then(function (response) {
                    $templateCache.put(templateUrl, response.data);
                });
        }
    ]);