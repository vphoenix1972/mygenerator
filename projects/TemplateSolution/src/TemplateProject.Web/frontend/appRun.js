'use strict';

function run($location) {
    'ngInject';

    // Goto loading page on application startup
    $location.url('/loading');
}

export default run;