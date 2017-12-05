'use strict';

import angular from 'angular';
import appModule from 'rootDir/appModule';

function HomeController() {
    'ngInject';

    const self = this;
    
    self.message = 'Hello World!';
}

angular.module(appModule)
    .controller('homeController', HomeController);