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

ConnectorService.prototype._http = function (url, method, params) {
    var self = this;

    return self._$http({
        url: url,
        method: method,
        params: params
    }).then(
        function (response) {
            return self._createResult(response);
        },
        function (response) {
            return self._handleError(response);
        });
};

ConnectorService.prototype._get = function (url) {
    var self = this;
    return self._$http.get(url).then(
        function (response) {
            return self._createResult(response);
        },
        function (response) {
            return self._handleError(response);
        });
};

ConnectorService.prototype._post = function (url, data) {
    var self = this;
    return self._$http.post(url, data).then(
        function (response) {
            return self._createResult(response);
        },
        function (response) {
            return self._handleError(response);
        });
};

ConnectorService.prototype._put = function (url, data) {
    var self = this;
    return self._$http.put(url, data).then(
        function (response) {
            return self._createResult(response);
        },
        function (response) {
            return self._handleError(response);
        });
};

ConnectorService.prototype._delete = function (url, data) {
    var self = this;
    return self._$http.delete(url, data).then(
        function (response) {
            return self._createResult(response);
        },
        function (response) {
            return self._handleError(response);
        });
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