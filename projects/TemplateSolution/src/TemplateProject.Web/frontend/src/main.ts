import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { EntryPointModule } from './app/entry-point.module';
import { environment } from './environments/environment';

import './utils/array-extensions';

if (environment.production) {
  enableProdMode();
}

platformBrowserDynamic().bootstrapModule(EntryPointModule)
  .catch(err => console.log(err));
