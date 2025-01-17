import { ApplicationConfig, importProvidersFrom, provideZoneChangeDetection } from '@angular/core';
import { provideRouter, withComponentInputBinding } from '@angular/router';
import { routes } from './app.routes';
import { ReactiveFormsModule } from '@angular/forms';
import { provideHttpClient, HTTP_INTERCEPTORS, withInterceptorsFromDi } from '@angular/common/http';
import { AuthInterceptor } from './shared/auth.interceptor';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { provideAnimations } from '@angular/platform-browser/animations';

import { ErrorStateMatcher, ShowOnDirtyErrorStateMatcher } from '@angular/material/core';

const ErrorState = {
  provide: ErrorStateMatcher,
  usecClass: ShowOnDirtyErrorStateMatcher
};


const AUTH_INTERCEPTOR = {
  provide: HTTP_INTERCEPTORS,
  useClass: AuthInterceptor,
  multi: true
};


export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes, withComponentInputBinding()),
    importProvidersFrom(ReactiveFormsModule),
    AUTH_INTERCEPTOR,
    provideHttpClient(withInterceptorsFromDi()), ErrorState, BrowserAnimationsModule,
    provideAnimations()
  ]
};




