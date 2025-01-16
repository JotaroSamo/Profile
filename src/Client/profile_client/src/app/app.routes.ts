
import { Routes } from '@angular/router';
import { RegisterUiComponent } from './component/register/register.component';
import { LoginUiComponent } from './component/login/login.component';
import { HelloPageComponent } from './component/hello-page/hello-page.component';


export const routes: Routes = [
  { path: '', component: HelloPageComponent },
  { path: 'register', component: RegisterUiComponent },
  { path: 'login', component: LoginUiComponent }
];
