import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { App } from './app/app';
import { HttpClientModule } from '@angular/common/http';
import { importProvidersFrom } from '../node_modules/@angular/core/index';
import { AppComponent } from './app/app.component';

bootstrapApplication(AppComponent, {
  providers: [
    importProvidersFrom(HttpClientModule)
  ]
});
