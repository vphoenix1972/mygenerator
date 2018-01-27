'use strict';

import appLayoutTemplateUrl from 'rootDir/pages/app/appLayout.tpl.html';
import homeTemplateUrl from 'rootDir/pages/app/home/home.tpl.html';
import todoIndexTemplateUrl from 'rootDir/pages/app/todo/todoIndex.tpl.html';
import todoEditTemplateUrl from 'rootDir/pages/app/todo/todoEdit.tpl.html';
import examplesTemplateUrl from 'rootDir/pages/app/examples/examples.tpl.html';
import aboutTemplateUrl from 'rootDir/pages/app/about/about.tpl.html';

function config(
    $stateProvider,
    $urlRouterProvider,
    $qProvider) {
    'ngInject';

    // Turn off unhandled rejection warnings
    // https://github.com/angular-ui/ui-router/issues/2889
    $qProvider.errorOnUnhandledRejections(false);

    configureRoutes($stateProvider, $urlRouterProvider);
}

function configureRoutes($stateProvider, $urlRouterProvider) {

    var appStateName = 'app';

    $stateProvider.state('app',
        {
            abstract: true,
            url: '/app',
            templateUrl: appLayoutTemplateUrl
        });

    $stateProvider.state('home',
        {
            parent: appStateName,
            url: '/home',
            controller: 'homeController',
            controllerAs: 'vm',
            templateUrl: homeTemplateUrl
        });

    $stateProvider.state('todo-index',
        {
            parent: appStateName,
            url: '/todo-index',
            controller: 'todoIndexController',
            controllerAs: 'vm',
            templateUrl: todoIndexTemplateUrl
        });

    $stateProvider.state('todo-new',
        {
            parent: appStateName,
            url: '/todo-new',
            controller: 'todoEditController',
            controllerAs: 'vm',
            templateUrl: todoEditTemplateUrl
        });

    $stateProvider.state('todo-edit',
        {
            parent: appStateName,
            url: '/todo-edit/{id:int}',
            controller: 'todoEditController',
            controllerAs: 'vm',
            templateUrl: todoEditTemplateUrl
        });

    $stateProvider.state('examples',
        {
            parent: appStateName,
            url: '/examples',
            controller: 'examplesController',
            controllerAs: 'vm',
            templateUrl: examplesTemplateUrl
        });

    $stateProvider.state('about',
        {
            parent: appStateName,
            url: '/about',
            controller: 'aboutController',
            controllerAs: 'vm',
            templateUrl: aboutTemplateUrl
        });

    // Set default and 404 state
    $urlRouterProvider.otherwise('/app/home');
}


export default config;