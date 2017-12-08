'use strict';

import homeTemplateUrl from 'rootDir/pages/home/home.tpl.html';
import todoIndexTemplateUrl from 'rootDir/pages/todo/todoIndex.tpl.html';
import examplesTemplateUrl from 'rootDir/pages/examples/examples.tpl.html';
import aboutTemplateUrl from 'rootDir/pages/about/about.tpl.html';

function config(
    $stateProvider,
    $urlRouterProvider,
    $qProvider) {
    'ngInject';

    // Turn off unhandled rejection warnings
    // https://github.com/angular-ui/ui-router/issues/2889
    $qProvider.errorOnUnhandledRejections(false);

    /* Configure routes */
    var homeState = {
        name: 'home',
        url: '/home',
        controller: 'homeController',
        controllerAs: 'vm',
        templateUrl: homeTemplateUrl
    };

    var todoIndexState = {
        name: 'todo-index',
        url: '/todo-index',
        controller: 'todoIndexController',
        controllerAs: 'vm',
        templateUrl: todoIndexTemplateUrl
    };

    var examplesState = {
        name: 'examples',
        url: '/examples',
        controller: 'examplesController',
        controllerAs: 'vm',
        templateUrl: examplesTemplateUrl
    };

    var aboutState = {
        name: 'about',
        url: '/about',
        controller: 'aboutController',
        controllerAs: 'vm',
        templateUrl: aboutTemplateUrl
    };

    $stateProvider.state(homeState)
                  .state(todoIndexState)
                  .state(examplesState)
                  .state(aboutState);

    // Set default and 404 state
    $urlRouterProvider.otherwise('/home');
}


export default config;