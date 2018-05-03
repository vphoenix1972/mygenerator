'use strict';

import angular from 'angular';

function run($location, $transitions, $state, authorizationService) {
    'ngInject';

    $transitions.onStart({},
        function (transition) {
            var user = authorizationService.currentUser();

            var state = transition.$to();

            var allowAnonymous = true;
            if (angular.isObject(state.data) &&
                angular.isArray(state.data.roles)) {
                allowAnonymous = false;
            }

            if (allowAnonymous)
                return true;

            if (!user.isAuthenticated) {
                return transition.router.stateService.target('signIn');
            }

            var allowedRoles = state.data.roles;

            var isAccessAllowed =
                user.roles.any(userRole => allowedRoles.any(allowedRole => allowedRole === userRole));

            if (!isAccessAllowed) {
                return transition.router.stateService.target('forbidden');
            }

            return true;
        });

    // Goto loading page on application startup
    $location.url('/loading');
}

export default run;