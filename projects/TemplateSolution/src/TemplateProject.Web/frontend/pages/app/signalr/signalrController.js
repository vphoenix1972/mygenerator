'use strict';

import * as signalR from '@aspnet/signalr';

import angular from 'angular';
import appModule from 'rootDir/appModule';

function SignalrController($scope,
    $timeout,
    connectorService) {
    'ngInject';

    const self = this;

    // Deps
    self._$scope = $scope;
    self._$timeout = $timeout;
    self._connectorService = connectorService;

    // Init
    self.messages = [];
    self._isDestroyed = false;
    self._reconnectTimer = null;
    self._isConnected = false;

    self._connection = new signalR.HubConnectionBuilder()
        .withUrl('/chatHub')
        .build();

    self._connection.on('ReceiveMessage',
        (user, message) => {
            self.messages.push({
                user: user,
                text: message
            });

            self._$scope.$apply();
        });

    self._connection.onclose(() => {
        self._isConnected = false;

        if (!self._isDestroyed)
            self._reconnect();
    });

    self._$scope.$on('$destroy',
        function () {
            self._isDestroyed = true;

            self._stopReconnect();

            self._connection.stop();

            self._isConnected = false;
        });

    self._connect();
}

SignalrController.prototype.isConnected = function () {
    const self = this;

    return self._isConnected;
}

SignalrController.prototype.onSendUsingApiButtonClicked = function () {
    const self = this;

    self._connectorService.signalrSendMessageAsync({ user: self.user, text: self.messageText });
}

SignalrController.prototype.onSendUsingSignalrConnectionButtonClicked = function () {
    const self = this;

    self._connection.invoke('SendMessage', self.user, self.messageText)
        .then((response) => console.log(response))
        .catch(err => console.error(err.toString()));
}

/* Private */

SignalrController.prototype._connect = function () {
    const self = this;

    self._isConnected = false;

    self._connection.start()
        .then(() => {
            self._isConnected = true;

            self._$scope.$apply();
        })
        .catch(err => {
            console.error(err.toString());

            if (!self._isDestroyed)
                self._reconnect();
        });
}

SignalrController.prototype._reconnect = function () {
    const self = this;

    if (self._reconnectTimer != null)
        return;

    self._reconnectionTimer = self._$timeout(() => {
            self._reconnectTimer = null;

            self._connect();
        },
        1000);
}

SignalrController.prototype._stopReconnect = function () {
    const self = this;

    if (self._reconnectTimer == null)
        return;

    self._reconnectTimer.cancel();

    self._reconnectTimer = null;
}

angular.module(appModule)
    .controller('signalrController', SignalrController);