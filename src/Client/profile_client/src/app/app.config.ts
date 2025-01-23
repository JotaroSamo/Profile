import { ApplicationConfig, importProvidersFrom, provideZoneChangeDetection } from '@angular/core';
import { provideRouter, withComponentInputBinding } from '@angular/router';
import { routes } from './app.routes';
import { ReactiveFormsModule } from '@angular/forms';
import { provideHttpClient, HTTP_INTERCEPTORS, withInterceptorsFromDi } from '@angular/common/http';
import { AuthInterceptor } from './shared/auth.interceptor';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { provideAnimations } from '@angular/platform-browser/animations';
import { COMMA, ENTER } from '@angular/cdk/keycodes';
import { ErrorStateMatcher, ShowOnDirtyErrorStateMatcher } from '@angular/material/core';
import { MAT_CHIPS_DEFAULT_OPTIONS } from '@angular/material/chips';
import { MatChipsModule } from '@angular/material/chips';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { NgModule } from '@angular/core';
const ErrorState = {
  provide: ErrorStateMatcher,
  useClass: ShowOnDirtyErrorStateMatcher
};

const chips = {
  provide: MAT_CHIPS_DEFAULT_OPTIONS,
  useValue: {
    separatorKeyCodes: [ENTER, COMMA]
  }
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
    provideAnimations(), chips, MatChipsModule, provideAnimationsAsync(), NgModule, provideAnimationsAsync()
  ]
};



