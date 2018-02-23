// ReSharper disable NativeTypePrototypeExtending

import angular from 'angular';

(function () {
    'use strict';

    if (!String.prototype.isNullOrEmpty) {
        String.prototype.isNullOrEmpty = function (str) {
            /// <summary>
            /// Checks if str is null or empty.
            /// </summary>

            return !angular.isString(str) || str.length < 1;
        }
    }

    if (!String.prototype.isNullOrWhiteSpace) {
        String.prototype.isNullOrWhiteSpace = function (str) {
            /// <summary>
            /// Checks if string is null, empty, or consists only of white-space characters.
            /// </summary>

            return !angular.isString(str) || str.trim().length < 1;
        }
    }
})();