'use strict';

import angular from 'angular';
import appModule from 'rootDir/appModule';

function ExamplesController(
    $q,
    dialogService
) {
    'ngInject';

    const self = this;

    self._$q = $q;
    self._dialogService = dialogService;
}

ExamplesController.prototype.onShowExecutingButtonClicked = function () {
    const self = this;

    self._dialogService.showExecutingAsync({
        title: 'Saving...',
        onCancelAsync: () => {
            return self._dialogService.showConfirmAsync({
                title: 'Are you sure to cancel saving?'
            });
        }
    });
}

ExamplesController.prototype.onShowSuccessButtonClicked = function () {
    const self = this;

    self._dialogService.showSuccess();
}

ExamplesController.prototype.onShowErrorButtonClicked = function () {
    const self = this;

    self._dialogService.showErrorAsync();
}

/* Private */


angular.module(appModule)
    .controller('examplesController', ExamplesController);