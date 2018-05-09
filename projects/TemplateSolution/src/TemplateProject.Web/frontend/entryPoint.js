'use strict';

import 'bootstrap.css';
import 'bootstrap.js';
import 'font-awesome.css';
import 'angularjs-toaster.css';


import './app.css';

import 'rootDir/utils/arrayExtensions';
import 'rootDir/utils/stringExtensions';

import './appModule';

import 'rootDir/services/connector/connectorService';
import 'rootDir/services/storage/localStorage';

import 'rootDir/directives/appSpinner/appSpinnerDirective';

import 'rootDir/dialogs/dialogService';
import 'rootDir/dialogs/progressDialog/progressDialogController';
import 'rootDir/dialogs/confirmDialog/confirmDialogController';
import 'rootDir/dialogs/errorDialog/errorDialogController';


import 'rootDir/pages/loading/loadingController';

import 'rootDir/pages/app/appLayoutController';
import 'rootDir/pages/app/home/homeController';
import 'rootDir/pages/app/todo/todoIndexController';
import 'rootDir/pages/app/todo/todoEditController';
import 'rootDir/pages/app/examples/examplesController';
import 'rootDir/pages/app/signalr/signalrController';
import 'rootDir/pages/app/about/aboutController';