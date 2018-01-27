'use strict';

import './forbidden.css';

import angular from 'angular';
import appModule from 'rootDir/appModule';

function ForbiddenController() {
    'ngInject';
}

angular.module(appModule)
    .controller('forbiddenController', ForbiddenController);