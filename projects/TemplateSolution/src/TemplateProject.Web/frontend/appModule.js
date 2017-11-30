'use strict';

import angular from 'angular';

import appConfig from './appConfig';
import appRun from './appRun';

var appModule = angular.module('templateProject',[

    ])
    .config(appConfig)
    .run(appRun);

export default appModule.name;