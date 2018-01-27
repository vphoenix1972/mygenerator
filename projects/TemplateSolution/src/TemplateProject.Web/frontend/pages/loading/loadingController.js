'use strict';

import './loading.css';

import angular from 'angular';
import appModule from 'rootDir/appModule';

function LoadingController($state, $rootScope, authorizationService) {
    'ngInject';

    const self = this;

    // Deps
    self._authorizationService = authorizationService;
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

    return self._authorizationService.loadUserFromCacheAsync()
        .then(() => {
            var user = self._authorizationService.currentUser();

            if (!user.isAuthenticated)
                self._$state.go('signIn');
            else
                self._$state.go('app.home');
        });
}

angular
    .module(appModule)
    .controller('loadingController', LoadingController);