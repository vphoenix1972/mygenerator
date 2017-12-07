'use strict';

import './appSpinner.css';

import templateUrl from './appSpinner.tpl.html';

import angular from 'angular';
import appModule from 'rootDir/appModule';

function AppSpinnerDirectiveController() {
    'ngInject';
}

AppSpinnerDirectiveController.prototype.$onInit = function () {

}

AppSpinnerDirectiveController.prototype.$postLink = function () {
}

AppSpinnerDirectiveController.prototype.$onDestroy = function () {

}

/* Private */


angular.module(appModule)
    .directive('appSpinner',
        function () {
            return {
                restrict: 'E',
                scope: {},
                controller: AppSpinnerDirectiveController,
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