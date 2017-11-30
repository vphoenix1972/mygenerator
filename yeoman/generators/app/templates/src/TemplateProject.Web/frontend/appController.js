'use strict';

import angular from 'angular';
import appModule from './appModule';

function AppController() {
    'ngInject';

    const self = this;
    
    self.message = 'Hello World!';
}

angular.module(appModule)
    .controller('appController', AppController);