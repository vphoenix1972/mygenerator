'use strict';

function run($location) {
    'ngInject';

    // Goto home page on application startup
    $location.url('/app/home');
}

export default run;