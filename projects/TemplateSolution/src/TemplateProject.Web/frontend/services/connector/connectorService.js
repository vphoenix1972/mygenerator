'use strict';

import angular from 'angular';
import appModule from 'rootDir/appModule';

function ConnectorService(
    $q,
    $http) {
    'ngInject';

    const self = this;

    self._$q = $q;
    self._$http = $http;
}

ConnectorService.prototype.setAccessToken = function (accessToken) {
    const self = this;

    self._accessToken = accessToken;
}

ConnectorService.prototype.getTodoIndexAsync = function () {
    const self = this;

    return self._get('todo/index');
}

ConnectorService.prototype.getTodoEditAsync = function (todoItemId) {
    const self = this;

    return self._get(`todo/edit/${todoItemId}`);
}

ConnectorService.prototype.addTodoItemAsync = function (item) {
    const self = this;

    return self._post('todo', item);
}

ConnectorService.prototype.updateTodoItemAsync = function (todoItemId, item) {
    const self = this;

    return self._put(`todo/${todoItemId}`, item);
}

ConnectorService.prototype.deleteTodoItemAsync = function (todoItemId) {
    const self = this;

    return self._delete(`todo/${todoItemId}`);
}

ConnectorService.prototype.signInAsync = function (options) {
    const self = this;

    return self._post('security/signin', options);
}

/* Private */

ConnectorService.prototype._http = function (options) {
    const self = this;

    if (angular.isString(self._accessToken)) {
        if (!angular.isObject(options.headers))
            options.headers = {};

        options.headers.Authorization = `Bearer ${self._accessToken}`;
    }

    return self._$http(options).then(
        function (response) {
            return self._createResult(response);
        },
        function (response) {
            return self._handleError(response);
        });
};

ConnectorService.prototype._get = function (url) {
    const self = this;

    return self._http({ method: 'GET', url: url });
};

ConnectorService.prototype._post = function (url, data) {
    const self = this;

    return self._http({ method: 'POST', url: url, data: data });
};

ConnectorService.prototype._put = function (url, data) {
    const self = this;

    return self._http({ method: 'PUT', url: url, data: data });
};

ConnectorService.prototype._delete = function (url, data) {
    const self = this;

    return self._http({ method: 'DELETE', url: url, data: data });
};

ConnectorService.prototype._handleError = function (response) {
    var self = this;

    return self._$q.reject(response);
};

ConnectorService.prototype._createResult = function (response) {
    return response;
};


angular.module(appModule)
    .service('connectorService', ConnectorService);