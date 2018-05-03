'use strict';

import './loading.css';

import templateUrl from './loading.tpl.html';

import angular from 'angular';
import appModule from 'rootDir/appModule';

function LoadingController($q, $state, $rootScope) {
    'ngInject';

    const self = this;

    // Deps
    self._$q = $q;
    self._$state = $state;

    // Init
    if ($rootScope.isApplicationLoaded) {
        $state.go('app.home');
        return;
    }

    self._loadApplicationAsync()
        .then(() => $rootScope.isApplicationLoaded = true);
}

/* Private */

LoadingController.prototype._loadApplicationAsync = function () {
    const self = this;

    self._$state.go('app.home');

    return self._$q.resolve();
}

angular
    .module(appModule)
    .controller('loadingController', LoadingController)
    // Preload dialog's template to show dialog even if server is unavailable
    .run([
        '$http', '$templateCache', function ($http, $templateCache) {
            $http.get(templateUrl)
                .then(function (response) {
                    $templateCache.put(templateUrl, response.data);
                });
        }
    ]);