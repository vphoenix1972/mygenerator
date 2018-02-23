'use strict';

import angular from 'angular';
import uiRouter from '@uirouter/angularjs';
import uiBootstrap from 'angular-ui-bootstrap';
import 'angularjs-toaster.js';
import angularJwt from 'angular-jwt';
   
import appConfig from './appConfig';
import appRun from './appRun';

var appModule = angular.module('<%= angularModuleName %>',[
        uiRouter,
        uiBootstrap,
        'toaster',
        angularJwt
    ])
    .config(appConfig)
    .run(appRun);

export default appModule.name;