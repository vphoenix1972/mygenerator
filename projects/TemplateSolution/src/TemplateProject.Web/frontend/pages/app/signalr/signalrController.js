'use strict';

import * as signalR from '@aspnet/signalr';

import angular from 'angular';
import appModule from 'rootDir/appModule';

function SignalrController($scope) {
    'ngInject';

    const self = this;

    // Deps
    self._$scope = $scope;

    // Init
    self.messages = [];

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

    self._connection.start().catch(err => console.error(err.toString()));
}

SignalrController.prototype.onSendButtonClicked = function () {
    const self = this;

    self._connection.invoke('SendMessage', self.user, self.messageText)
        .catch(err => console.error(err.toString()));
}

angular.module(appModule)
    .controller('signalrController', SignalrController);