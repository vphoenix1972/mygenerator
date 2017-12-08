'use strict';

import angular from 'angular';
import uiRouter from '@uirouter/angularjs';
import uiBootstrap from 'angular-ui-bootstrap';
import 'angularjs-toaster.js';

import appConfig from './appConfig';
import appRun from './appRun';

var appModule = angular.module('<%= angularModuleName %>',[
        uiRouter,
        uiBootstrap,
        'toaster'
    ])
    .config(appConfig)
    .run(appRun);

export default appModule.name;