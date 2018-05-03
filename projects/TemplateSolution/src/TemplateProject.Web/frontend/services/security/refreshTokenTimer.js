'use strict';

import angular from 'angular';

function RefreshTokenTimer(
    $timeout,
    $log,
    connectorService,
    securityConstants) {

    'ngInject';

    const self = this;

    // Deps
    self._$timeout = $timeout;
    self._$log = $log;
    self._connectorService = connectorService;

    // Init
    self._timespanToRefreshTokenBeforeExpiration = securityConstants.timespanToRefreshTokenBeforeExpiration;

    self.onTokensRenewed = null;
    self.onError = null;

    self._resetState();
}

RefreshTokenTimer.prototype.start = function (options) {
    const self = this;

    self.stop();

    self._refreshToken = options.refreshToken;

    self._accessTokenExpires = options.accessTokenData.exp * 1000;

    var now = self._getNow();

    if (now >= self._accessTokenExpires)
        throw new Error('Cannot start refresh token timer - access token is expired');

    var delay = self._getTimerDelay(now);

    self._startTimer(delay);
}

RefreshTokenTimer.prototype.stop = function () {
    const self = this;

    if (self._refreshTimerPromise == null)
        return;

    self._$timeout.cancel(self._refreshTimerPromise);

    self._resetState();
}

/* Private */

RefreshTokenTimer.prototype._startTimer = function (delay) {
    const self = this;

    self._refreshTimerPromise = self._$timeout(() => {
            self._connectorService.refreshToken({ refreshToken: self._refreshToken })
                .then(response => self._onRefreshTokenSuccess(response),
                    response => self._onRefreshTokenError(response));
        },
        delay);
}

RefreshTokenTimer.prototype._onRefreshTokenSuccess = function (response) {
    const self = this;

    self._resetState();

    self._raiseOnTokensRenewed(response.data);
}

RefreshTokenTimer.prototype._onRefreshTokenError = function (response) {
    const self = this;

    var refreshTokenInvalid = response.status >= 400 && response.status < 500;
    if (refreshTokenInvalid) {
        self._resetState();

        self._raiseOnError();

        return;
    }

    var now = self._getNow();

    if (now >= self._accessTokenExpires) {
        self._$log.error('Failed to refresh access token after refresh token error: access token expired');

        self._resetState();

        self._raiseOnError();

        return;
    }

    const delay = self._getTimerDelay(now);

    self._startTimer(delay);
}

RefreshTokenTimer.prototype._getTimerDelay = function (now) {
    const self = this;

    var checkTime;
    if (self._accessTokenExpires - now > self._timespanToRefreshTokenBeforeExpiration) {
        checkTime = self._accessTokenExpires - self._timespanToRefreshTokenBeforeExpiration;
    } else {
        checkTime = Math.floor(now + (self._accessTokenExpires - now) / 2);
    }

    return checkTime - now;
}

RefreshTokenTimer.prototype._resetState = function () {
    const self = this;

    self._refreshTimerPromise = null;
    self._refreshToken = null;
}

RefreshTokenTimer.prototype._raiseOnTokensRenewed = function (tokens) {
    const self = this;

    if (!angular.isFunction(self.onTokensRenewed))
        return;

    self.onTokensRenewed(tokens);
}

RefreshTokenTimer.prototype._raiseOnError = function () {
    const self = this;

    if (!angular.isFunction(self.onError))
        return;

    self.onError();
}

RefreshTokenTimer.prototype._getNow = function () {
    return Date.now();
}


export default RefreshTokenTimer;