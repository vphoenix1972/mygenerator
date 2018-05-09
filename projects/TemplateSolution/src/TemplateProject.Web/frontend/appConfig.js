'use strict';

import loadingTemplateUrl from 'rootDir/pages/loading/loading.tpl.html';

import appLayoutTemplateUrl from 'rootDir/pages/app/appLayout.tpl.html';
import homeTemplateUrl from 'rootDir/pages/app/home/home.tpl.html';
import todoIndexTemplateUrl from 'rootDir/pages/app/todo/todoIndex.tpl.html';
import todoEditTemplateUrl from 'rootDir/pages/app/todo/todoEdit.tpl.html';
import examplesTemplateUrl from 'rootDir/pages/app/examples/examples.tpl.html';
import signalrTemplateUrl from 'rootDir/pages/app/signalr/signalr.tpl.html';
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
    configureCommonRoutes($stateProvider, $urlRouterProvider);

    configureAppRoutes($stateProvider, $urlRouterProvider);

    // Set default and 404 state
    $urlRouterProvider.otherwise('/app/home');
}

function configureCommonRoutes($stateProvider) {
    $stateProvider.state('loading',
        {
            url: '/loading',
            controller: 'loadingController',
            controllerAs: 'vm',
            templateUrl: loadingTemplateUrl
        });
}

function configureAppRoutes($stateProvider) {
    const appStateName = 'app';

    $stateProvider.state(appStateName,
        {
            abstract: true,
            url: '/app',
            controller: 'appLayoutController',
            controllerAs: 'vm',
            templateUrl: appLayoutTemplateUrl
        });

    $stateProvider.state(`${appStateName}.home`,
        {
            url: '/home',
            controller: 'homeController',
            controllerAs: 'vm',
            templateUrl: homeTemplateUrl
        });

    $stateProvider.state(`${appStateName}.todo-index`,
        {
            url: '/todo-index',
            controller: 'todoIndexController',
            controllerAs: 'vm',
            templateUrl: todoIndexTemplateUrl
        });

    $stateProvider.state(`${appStateName}.todo-new`,
        {
            url: '/todo-new',
            controller: 'todoEditController',
            controllerAs: 'vm',
            templateUrl: todoEditTemplateUrl
        });

    $stateProvider.state(`${appStateName}.todo-edit`,
        {
            url: '/todo-edit/{id:int}',
            controller: 'todoEditController',
            controllerAs: 'vm',
            templateUrl: todoEditTemplateUrl
        });

    $stateProvider.state(`${appStateName}.examples`,
        {
            url: '/examples',
            controller: 'examplesController',
            controllerAs: 'vm',
            templateUrl: examplesTemplateUrl
        });

    $stateProvider.state(`${appStateName}.signalr`,
        {
            url: '/signalr',
            controller: 'signalrController',
            controllerAs: 'vm',
            templateUrl: signalrTemplateUrl
        });

    $stateProvider.state(`${appStateName}.about`,
        {
            url: '/about',
            controller: 'aboutController',
            controllerAs: 'vm',
            templateUrl: aboutTemplateUrl
        });
}



export default config;