'use strict';

import angular from 'angular';
import appModule from 'rootDir/appModule';

function LocalStorage(
    $window,
    $log) {

    'ngInject';

    const self = this;

    // Deps
    self._$window = $window;
    self._$log = $log;
}

LocalStorage.prototype.getItem = function (key) {
    const self = this;

    var valueJson = self._$window.localStorage.getItem(key);
    var value = null;

    try {
        value = JSON.parse(valueJson);
    } catch (e) {
        self._$log.warn(`Error parsing '${key}' from local storage: ${e.name}:${e.message}\n${e.stack}`);
    }

    return value;
}

LocalStorage.prototype.setItem = function (key, value) {
    const self = this;

    var valueJson = JSON.stringify(value);

    self._$window.localStorage.setItem(key, valueJson);
}

LocalStorage.prototype.removeItem = function (key) {
    const self = this;

    self._$window.localStorage.removeItem(key);
}

/* Private */


angular.module(appModule)
    .service('localStorage', LocalStorage);