import { bootstrapApplication } from '@angular/platform-browser';
import { App } from './app/app';
import { config } from './app/app.config.server';
import { HttpClientModule } from '@angular/common/http';
import { importProvidersFrom } from '@angular/core';

const bootstrap = () => bootstrapApplication(App, config);
bootstrapApplication(AppComponent, {
  providers: [importProvidersFrom(HttpClientModule)]
});
export default bootstrap;
