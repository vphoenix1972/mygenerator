'use strict';

import angular from 'angular';
import appModule from 'rootDir/appModule';

angular.module(appModule)
    .constant('securityConstants',
        {
            timespanToRefreshTokenBeforeExpiration: 30 * 1000, // 30 sec
            localStorageRefreshTokenKey: 'authorizationService.refreshToken'
        });