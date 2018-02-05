'use strict';

import 'bootstrap.css';
import 'bootstrap.js';
import 'font-awesome.css';
import 'angularjs-toaster.css';


import './app.css';

import 'rootDir/utils/arrayExtensions';
import 'rootDir/utils/stringExtensions';

import './appModule';

import 'rootDir/constants/roles';

import 'rootDir/services/connectorService';
import 'rootDir/services/security/authorizationService';
import 'rootDir/services/storage/localStorage';

import 'rootDir/directives/appSpinner/appSpinnerDirective';

import 'rootDir/dialogs/dialogService';
import 'rootDir/dialogs/progressDialog/progressDialogController';
import 'rootDir/dialogs/confirmDialog/confirmDialogController';
import 'rootDir/dialogs/errorDialog/errorDialogController';


import 'rootDir/pages/loading/loadingController';
import 'rootDir/pages/signIn/signInController';
import 'rootDir/pages/forbidden/forbiddenController';
import 'rootDir/pages/register/registerController';

import 'rootDir/pages/app/appLayoutController';
import 'rootDir/pages/app/home/homeController';
import 'rootDir/pages/app/todo/todoIndexController';
import 'rootDir/pages/app/todo/todoEditController';
import 'rootDir/pages/app/examples/examplesController';
import 'rootDir/pages/app/about/aboutController';