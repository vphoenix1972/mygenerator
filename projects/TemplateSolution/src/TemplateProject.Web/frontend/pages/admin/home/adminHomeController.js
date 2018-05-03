'use strict';

import angular from 'angular';
import appModule from 'rootDir/appModule';

function AdminHomeController() {
    'ngInject';

    const self = this;    
}

angular.module(appModule)
    .controller('adminHomeController', AdminHomeController);