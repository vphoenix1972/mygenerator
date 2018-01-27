'use strict';

import './loading.css';

import angular from 'angular';
import appModule from 'rootDir/appModule';

function LoadingController($timeout, $state) {
    'ngInject';

    const self = this;

    $timeout(() => $state.go('app.home'), 1);
}

/* Private */

angular
    .module(appModule)
    .controller('loadingController', LoadingController);