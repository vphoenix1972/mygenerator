'use strict';

import angular from 'angular';
import appModule from 'rootDir/appModule';

function AboutController() {
    'ngInject';

    const self = this;
}

angular.module(appModule)
    .controller('aboutController', AboutController);