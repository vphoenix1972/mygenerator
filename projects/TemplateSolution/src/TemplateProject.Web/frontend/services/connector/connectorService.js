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

ConnectorService.prototype.getTodoIndexAsync = function () {
    const self = this;

    return self._get('app/todo/index');
}

ConnectorService.prototype.getTodoEditAsync = function (todoItemId) {
    const self = this;

    return self._get(`app/todo/edit/${todoItemId}`);
}

ConnectorService.prototype.addTodoItemAsync = function (item) {
    const self = this;

    return self._post('app/todo', item);
}

ConnectorService.prototype.updateTodoItemAsync = function (todoItemId, item) {
    const self = this;

    return self._put(`app/todo/${todoItemId}`, item);
}

ConnectorService.prototype.deleteTodoItemAsync = function (todoItemId) {
    const self = this;

    return self._delete(`app/todo/${todoItemId}`);
}

ConnectorService.prototype.signalrSendMessageAsync = function (message) {
    const self = this;

    return self._post('app/signalr/message', message);
}

/* Private */

ConnectorService.prototype._http = function (options) {
    const self = this;

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