'use strict';

import './appUserSettingsPasswordChange.css';

import templateUrl from './appUserSettingsPasswordChange.tpl.html';

import angular from 'angular';
import appModule from 'rootDir/appModule';

function AppUserSettingsPasswordChangeDirectiveController(authorizationService,
    dialogService) {
    'ngInject';

    const self = this;

    // Deps
    self._authorizationService = authorizationService;
    self._dialogService = dialogService;

    // Init
    self.passwordChangeError = null;
}

AppUserSettingsPasswordChangeDirectiveController.prototype.$onInit = function () {

}

AppUserSettingsPasswordChangeDirectiveController.prototype.$postLink = function () {

}

AppUserSettingsPasswordChangeDirectiveController.prototype.$onDestroy = function () {

}

AppUserSettingsPasswordChangeDirectiveController.prototype.onSaveButtonClicked = function () {
    const self = this;

    self._validateInput();
    if (self.passwordChangeError)
        return;

    self._dialogService.showExecutingForSaveAsync();

    var request = {
        oldPassword: self.oldPassword,
        newPassword: self.newPassword
    };

    self._authorizationService.changePasswordAsync(request)
        .then(
            () => self._dialogService.showSuccess(),
            (errorMessage) => self.passwordChangeError = errorMessage
        )
        .finally(() => self._dialogService.hideExecuting());
}

/* Private */

AppUserSettingsPasswordChangeDirectiveController.prototype._validateInput = function () {
    const self = this;

    self.passwordChangeError = null;

    if (String.prototype.isNullOrWhiteSpace(self.oldPassword)) {
        self.passwordChangeError = 'Old password cannot be empty';
        return;
    }

    if (String.prototype.isNullOrWhiteSpace(self.newPassword)) {
        self.passwordChangeError = 'New password cannot be empty';
        return;
    }

    if (self.newPassword !== self.newPasswordConfirmation) {
        self.passwordChangeError = 'New passwords are not equal';
        return;
    }
}


angular.module(appModule)
    .directive('appUserSettingsPasswordChange',
        function () {
            return {
                restrict: 'E',
                scope: {},
                controller: AppUserSettingsPasswordChangeDirectiveController,
                controllerAs: 'vm',
                bindToController: true,
                templateUrl: templateUrl
            }
        })
    // Preload directive's template to prevent blinking of the directive
    // during the first load of page
    .run([
        '$http', '$templateCache', function ($http, $templateCache) {
            $http.get(templateUrl)
                .then(function (response) {
                    $templateCache.put(templateUrl, response.data);
                });
        }
    ]);