// ReSharper disable NativeTypePrototypeExtending

import angular from 'angular';

(function () {
    'use strict';

    Array.prototype.any = function (predicate) {
        /// <summary>
        /// Determines whether any element of a sequence satisfies a condition.
        /// </summary>

        var self = this;

        if (angular.isFunction(predicate)) {
            for (var i = 0; i < self.length; i++) {
                var item = self[i];
                if (predicate(item))
                    return true;
            }

            return false;
        }

        return self.length > 0;
    };

    Array.prototype.all = function (predicate) {
        /// <summary>
        /// Determines whether any element of a sequence satisfies a condition.
        /// </summary>

        var self = this;

        if (!angular.isFunction(predicate))
            throw new Error('Argument "predicate" cannot be null.');

        for (var i = 0; i < self.length; i++) {
            var item = self[i];
            if (!predicate(item))
                return false;
        }

        return true;
    };

    Array.prototype.remove = function (value) {
        /// <summary>
        /// Removes the value from the array if value can be found.
        /// </summary>

        var self = this;

        var index = self.indexOf(value);
        if (index < 0)
            return;

        self.splice(index, 1);
    }

    Array.prototype.removeRange = function (values) {
        /// <summary>
        /// For each item in values, removes this item from the array if it can be found.
        /// </summary>

        var self = this;

        if (!angular.isArray(values))
            throw new Error('Argument "values" must be an array');

        values.forEach(function (item) {
            self.remove(item);
        });
    }

    Array.prototype.removeByPredicate = function (predicate, removeAll) {
        /// <summary>
        /// Removes the value from the array using predicate.
        /// </summary>

        var self = this;

        for (var i = 0; i < self.length;) {
            var item = self[i];

            if (predicate(item)) {
                self.splice(i, 1);

                if (removeAll)
                    continue;

                return;
            }

            i++;
        }
    }

    Array.prototype.removeAllByPredicate = function (predicate) {
        /// <summary>
        /// Removes all values from the array which satisfy predicate.
        /// </summary>

        var self = this;

        self.removeByPredicate(predicate, 'removeAll');
    }

    Array.prototype.removeLast = function (count, removeCallback) {
        /// <summary>
        /// Removes the value from the array using predicate.
        /// </summary>

        var self = this;

        if (self.length < 1)
            return;

        if (!angular.isNumber(count)) {
            self.splice(-1, 1);
            return;
        }

        if (!angular.isFunction(removeCallback)) {
            self.splice(-1, count);
            return;
        }

        var lastIndex = self.length - count;
        for (var i = self.length - 1; i >= lastIndex && i >= 0; i--) {
            var element = self[i];

            removeCallback(element);

            self.splice(-1, 1);
        }
    }

    Array.prototype.orderBy = function (comparer) {
        /// <summary>
        /// The orderBy() method orders the elements of an array using comparer and returns the array.        
        /// </summary>

        return this.sort(comparer);
    };

    Array.prototype.merge = function (newArray, options) {
        /// <summary>
        /// Merges array with another array and returns new array. Does not modify given arrays.
        /// Pass options.createNew === null to prevent adding of new items.
        /// </summary>
        var self = this;

        /* Check arguments */
        if (!angular.isArray(newArray))
            throw new Error('Argument "newArray" must be an array');

        var defaultOptions = {
            predicate: function (newItem, oldItem) { return newItem === oldItem; },
            // ReSharper disable once UnusedParameter
            merge: function (newItem, oldItem) { return oldItem; },
            createNew: function (newItem) { return newItem; },
            onRemove: undefined,
            isCreateNewAllowed: true,
            isRemoveAllowed: true
        }

        var currentOptions = defaultOptions;
        if (options != null) {
            if (angular.isFunction(options.predicate))
                currentOptions.predicate = options.predicate;

            if (angular.isFunction(options.merge))
                currentOptions.merge = options.merge;

            if (angular.isFunction(options.createNew))
                currentOptions.createNew = options.createNew;

            if (angular.isFunction(options.onRemove))
                currentOptions.onRemove = options.onRemove;

            if (angular.isDefined(options.isCreateNewAllowed))
                currentOptions.isCreateNewAllowed = options.isCreateNewAllowed;

            if (angular.isDefined(options.isRemoveAllowed))
                currentOptions.isRemoveAllowed = options.isRemoveAllowed;
        }

        /* Merge */
        var result = [];

        var isRemoveAllowed = currentOptions.isRemoveAllowed;
        if (angular.isFunction(currentOptions.isRemoveAllowed))
            isRemoveAllowed = currentOptions.isRemoveAllowed(newArray, this);

        if (isRemoveAllowed) {
            self.forEach(function (oldItem) {
                var newItem = newArray.find(function (newIt) { return currentOptions.predicate(newIt, oldItem); });
                if (newItem != null)
                    return; // Item still exists in new array

                if (angular.isFunction(currentOptions.onRemove))
                    currentOptions.onRemove(oldItem);
            });

            // Create merge result
            newArray.forEach(function (newItem) {
                var oldItem = self.find(function (oldIt) { return currentOptions.predicate(newItem, oldIt); });
                if (oldItem != null) {
                    result.push(currentOptions.merge(newItem, oldItem));
                    return;
                }

                var isCreateNewAllowed = currentOptions.isCreateNewAllowed;
                if (angular.isFunction(currentOptions.isCreateNewAllowed))
                    isCreateNewAllowed = currentOptions.isCreateNewAllowed(newItem);

                if (isCreateNewAllowed)
                    result.push(currentOptions.createNew(newItem));
            });
        } else {
            self.forEach(function (oldItem) {
                var newItem = newArray.find(function (newIt) { return currentOptions.predicate(newIt, oldItem); });
                if (newItem != null) {
                    result.push(currentOptions.merge(newItem, oldItem));
                    return;
                }

                result.push(oldItem);
            });

            newArray.forEach(function (newItem) {
                var oldItem = self.find(function (oldIt) { return currentOptions.predicate(newItem, oldIt); });
                if (oldItem != null) {
                    return; // Merged already
                }

                var isCreateNewAllowed = currentOptions.isCreateNewAllowed;
                if (angular.isFunction(currentOptions.isCreateNewAllowed))
                    isCreateNewAllowed = currentOptions.isCreateNewAllowed(newItem);

                if (isCreateNewAllowed)
                    result.push(currentOptions.createNew(newItem));
            });
        }

        return result;
    }

    if (!Array.prototype.last) {
        Array.prototype.last = function () {
            /// <summary>
            /// Returns last element in the array.
            /// </summary>

            var self = this;

            return self[self.length - 1];
        };
    };

    Array.prototype.contains = function (searchElement /*, fromIndex*/) {
        /// <summary>
        /// Determines whether an array includes a certain element, returning true or false as appropriate.
        /// </summary>

        var o = Object(this);
        var len = parseInt(o.length) || 0;
        if (len === 0) {
            return false;
        }
        var n = parseInt(arguments[1]) || 0;
        var k;
        if (n >= 0) {
            k = n;
        } else {
            k = len + n;
            if (k < 0) {
                k = 0;
            }
        }
        while (k < len) {
            var currentElement = o[k];
            if (searchElement === currentElement ||
                    // Code taken from https://developer.mozilla.org/ru/docs/Web/JavaScript/Reference/Global_Objects/Array/includes
                    // ReSharper disable SimilarExpressionsComparison
                    (searchElement !== searchElement && currentElement !== currentElement)
                // ReSharper restore SimilarExpressionsComparison
            ) {
                return true;
            }
            k++;
        }
        return false;
    };

    if (!Array.prototype.containsRepeatingElements) {
        Array.prototype.containsRepeatingElements = function (predicate) {
            /// <summary>
            /// Determines if an array has repeating elements.
            /// </summary>

            var self = this;

            if (!angular.isFunction(predicate))
                predicate = (element1, element2) => element1 === element2;

            for (var i = 0; i < self.length - 1; i++) {
                for (var j = i + 1; j < self.length; j++) {
                    if (predicate(self[i], self[j])) {
                        return {
                            index1: i,
                            index2: j
                        }
                    }
                }
            }

            return false;
        }
    }

    if (!Array.prototype.firstOrDefault) {
        Array.prototype.firstOrDefault = function (predicate, defaultValue = null) {
            /// <summary>
            /// Returns the first element of the sequence that satisfies a condition or a default
            /// value if no such element is found.
            /// </summary>

            var self = this;

            if (angular.isFunction(predicate)) {

                for (var i = 0; i < self.length; i++) {
                    if (predicate(self[i]))
                        return self[i];
                }

                return defaultValue;
            } else {
                return self.length > 0 ? self[0] : defaultValue;
            }
        };
    }

    if (!Array.prototype.pushArray) {
        Array.prototype.pushArray = function (items) {
            /// <summary>
            /// Adds an array to the end of an array and returns the new length of the array.
            /// </summary>

            var self = this;

            if (!angular.isArray(items))
                throw new Error('Argument "items" must be an array.');

            return Array.prototype.push.apply(self, items);
        };
    }

    if (!Array.prototype.clear) {
        Array.prototype.clear = function () {
            /// <summary>
            /// Removes all elements from the array.
            /// </summary>

            var self = this;

            self.splice(0, self.length);
        };
    }
})();