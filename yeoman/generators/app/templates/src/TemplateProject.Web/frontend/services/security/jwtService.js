'use strict';

import angular from 'angular';
import appModule from 'rootDir/appModule';

function JwtService(
    jwtHelper) {

    'ngInject';

    const self = this;

    // Deps
    self._jwtHelper = jwtHelper;
}

JwtService.prototype.decodeToken = function (token) {
    const self = this;

    return self._jwtHelper.decodeToken(token);
}

/* Private */


angular.module(appModule)
    .service('jwtService', JwtService);