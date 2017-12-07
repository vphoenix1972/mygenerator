'use strict';

import homeTemplateUrl from 'rootDir/pages/home/home.tpl.html';
import aboutTemplateUrl from 'rootDir/pages/about/about.tpl.html';
import todoIndexTemplateUrl from 'rootDir/pages/todo/todoIndex.tpl.html';

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

    var todoIndexState = {
        name: 'todo-index',
        url: '/todo-index',
        controller: 'todoIndexController',
        controllerAs: 'vm',
        templateUrl: todoIndexTemplateUrl
    };

    $stateProvider.state(homeState)
                  .state(aboutState)
                  .state(todoIndexState);

    // Set default and 404 state
    $urlRouterProvider.otherwise('/home');
}


export default config;