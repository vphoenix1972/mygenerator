import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { AppModule } from './app/appModule';

const platform = platformBrowserDynamic();
platform.bootstrapModule(AppModule);