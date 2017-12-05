'use strict';

import homeTemplateUrl from 'rootDir/pages/home/home.tpl.html';
import aboutTemplateUrl from 'rootDir/pages/about/about.tpl.html';

function config($stateProvider, $urlRouterProvider) {
    'ngInject';

    /* Configure routes */
    var homeState = {
        name: 'home',
        url: '/home',
        controller: 'homeController',
        controllerAs: 'vm',
        templateUrl: homeTemplateUrl
    };

    var aboutState = {
        name: 'about',
        url: '/about',
        controller: 'aboutController',
        controllerAs: 'vm',
        templateUrl: aboutTemplateUrl
    };

    $stateProvider.state(homeState)
                  .state(aboutState);

    // Set default and 404 state
    $urlRouterProvider.otherwise('/home');
}


export default config;