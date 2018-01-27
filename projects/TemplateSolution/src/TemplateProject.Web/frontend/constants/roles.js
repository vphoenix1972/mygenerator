'use strict';

import angular from 'angular';
import appModule from 'rootDir/appModule';

angular.module(appModule)
    .constant('roles',
        {
            user: 'user',
            admin: 'admin'
        });