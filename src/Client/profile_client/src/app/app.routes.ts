// app.routes.ts
import { Routes } from '@angular/router';
import { RegisterComponent } from './register-ui/register-ui.component';
import { LoginComponent } from './login-ui/login-ui.component';

export const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent }
];
