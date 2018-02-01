'use strict';

import loadingTemplateUrl from 'rootDir/pages/loading/loading.tpl.html';
import signInTemplateUrl from 'rootDir/pages/signIn/signIn.tpl.html';
import forbiddenTemplateUrl from 'rootDir/pages/forbidden/forbidden.tpl.html';

import appLayoutTemplateUrl from 'rootDir/pages/app/appLayout.tpl.html';
import homeTemplateUrl from 'rootDir/pages/app/home/home.tpl.html';
import todoIndexTemplateUrl from 'rootDir/pages/app/todo/todoIndex.tpl.html';
import todoEditTemplateUrl from 'rootDir/pages/app/todo/todoEdit.tpl.html';
import examplesTemplateUrl from 'rootDir/pages/app/examples/examples.tpl.html';
import aboutTemplateUrl from 'rootDir/pages/app/about/about.tpl.html';

function config(
    $stateProvider,
    $urlRouterProvider,
    $qProvider,
    roles) {
    'ngInject';

    // Turn off unhandled rejection warnings
    // https://github.com/angular-ui/ui-router/issues/2889
    $qProvider.errorOnUnhandledRejections(false);

    configureRoutes($stateProvider, $urlRouterProvider, roles);
}

function configureRoutes($stateProvider, $urlRouterProvider, roles) {

    var appStateName = 'app';
    var authenticatedUserRoles = [roles.user];

    $stateProvider.state('loading',
        {
            url: '/loading',
            controller: 'loadingController',
            controllerAs: 'vm',
            templateUrl: loadingTemplateUrl
        });

    $stateProvider.state('signIn',
        {
            url: '/signIn',
            controller: 'signInController',
            controllerAs: 'vm',
            templateUrl: signInTemplateUrl
        });

    $stateProvider.state('forbidden',
        {
            url: '/forbidden',
            controller: 'forbiddenController',
            controllerAs: 'vm',
            templateUrl: forbiddenTemplateUrl,
            data: {
                roles: authenticatedUserRoles
            }
        });

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
            templateUrl: homeTemplateUrl,
            data: {
                roles: authenticatedUserRoles
            }
        });

    $stateProvider.state(`${appStateName}.todo-index`,
        {
            url: '/todo-index',
            controller: 'todoIndexController',
            controllerAs: 'vm',
            templateUrl: todoIndexTemplateUrl,
            data: {
                roles: authenticatedUserRoles
            }
        });

    $stateProvider.state(`${appStateName}.todo-new`,
        {
            url: '/todo-new',
            controller: 'todoEditController',
            controllerAs: 'vm',
            templateUrl: todoEditTemplateUrl,
            data: {
                roles: authenticatedUserRoles
            }
        });

    $stateProvider.state(`${appStateName}.todo-edit`,
        {
            url: '/todo-edit/{id:int}',
            controller: 'todoEditController',
            controllerAs: 'vm',
            templateUrl: todoEditTemplateUrl,
            data: {
                roles: authenticatedUserRoles
            }
        });

    $stateProvider.state(`${appStateName}.examples`,
        {
            url: '/examples',
            controller: 'examplesController',
            controllerAs: 'vm',
            templateUrl: examplesTemplateUrl,
            data: {
                roles: [roles.admin]
            }
        });

    $stateProvider.state(`${appStateName}.about`,
        {
            url: '/about',
            controller: 'aboutController',
            controllerAs: 'vm',
            templateUrl: aboutTemplateUrl,
            data: {
                roles: authenticatedUserRoles
            }
        });

    // Set default and 404 state
    $urlRouterProvider.otherwise('/app/home');
}


export default config;