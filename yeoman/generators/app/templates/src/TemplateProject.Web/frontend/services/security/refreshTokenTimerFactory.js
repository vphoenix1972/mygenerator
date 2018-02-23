'use strict';

import RefreshTokenTimer from './refreshTokenTimer';

import angular from 'angular';
import appModule from 'rootDir/appModule';

function RefreshTokenTimerFactory(
    $injector) {

    'ngInject';

    const self = this;

    // Deps
    self._$injector = $injector;
}

RefreshTokenTimerFactory.prototype.create = function () {
    const self = this;

    return self._$injector.instantiate(RefreshTokenTimer);
}

/* Private */


angular.module(appModule)
    .service('refreshTokenTimerFactory', RefreshTokenTimerFactory);