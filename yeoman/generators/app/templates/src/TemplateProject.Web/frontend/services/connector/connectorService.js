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

/* Security */

ConnectorService.prototype.signInAsync = function (data) {
    const self = this;

    return self._post('security/signin', data);
}

ConnectorService.prototype.registerAsync = function (data) {
    const self = this;

    return self._post('security/register', data);
}

ConnectorService.prototype.changePasswordAsync = function (userId, data) {
    const self = this;

    return self._post(`app/user/${userId}/changePassword`, data);
}

ConnectorService.prototype.refreshToken = function (data) {
    const self = this;

    return self._$http({
        method: 'POST',
        url: 'security/refreshToken',
        data: data
    });
}

ConnectorService.prototype.signOutAsync = function (data) {
    const self = this;

    return self._post('security/signout', data);
}

/* Administration */

ConnectorService.prototype.getUsersIndexAsync = function () {
    const self = this;

    return self._get('admin/users/index');
}

ConnectorService.prototype.deleteUserAsync = function (userId) {
    const self = this;

    return self._delete(`admin/users/${userId}`);
}

/* Private */

ConnectorService.prototype._http = function (options) {
    const self = this;

    if (angular.isString(self._accessToken)) {
        if (!angular.isObject(options.headers))
            options.headers = {};

        options.headers.Authorization = self._createAuthorizationHeader(self._accessToken);
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

ConnectorService.prototype._createAuthorizationHeader = function (accessToken) {
    return `Bearer ${accessToken}`;
}


angular.module(appModule)
    .service('connectorService', ConnectorService);