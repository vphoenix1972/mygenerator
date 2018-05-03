'use strict';

import './forbidden.css';

import templateUrl from './forbidden.tpl.html';

import angular from 'angular';
import appModule from 'rootDir/appModule';

function ForbiddenController() {
    'ngInject';
}

angular.module(appModule)
    .controller('forbiddenController', ForbiddenController)
    // Preload dialog's template to show dialog even if server is unavailable
    .run([
        '$http', '$templateCache', function ($http, $templateCache) {
            $http.get(templateUrl)
                .then(function (response) {
                    $templateCache.put(templateUrl, response.data);
                });
        }
    ]);