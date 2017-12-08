'use strict';

import 'bootstrap.css';
import 'bootstrap.js';
import 'font-awesome.css';
import 'angularjs-toaster.css';


import './app.css';

import './appModule';

import 'rootDir/services/connectorService';

import 'rootDir/directives/appSpinner/appSpinnerDirective';

import 'rootDir/dialogs/dialogService';
import 'rootDir/dialogs/progressDialog/progressDialogController';
import 'rootDir/dialogs/confirmDialog/confirmDialogController';
import 'rootDir/dialogs/errorDialog/errorDialogController';

import 'rootDir/pages/home/homeController';
import 'rootDir/pages/todo/todoIndexController';
import 'rootDir/pages/examples/examplesController';
import 'rootDir/pages/about/aboutController';