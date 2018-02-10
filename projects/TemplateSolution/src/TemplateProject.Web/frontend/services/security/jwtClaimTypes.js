'use strict';

import angular from 'angular';
import appModule from 'rootDir/appModule';

angular.module(appModule)
    .constant('jwtClaimTypes',
        {
            name: 'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name',
            role: 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'
        });