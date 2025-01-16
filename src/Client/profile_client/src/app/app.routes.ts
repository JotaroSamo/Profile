
import { Routes } from '@angular/router';
import { RegisterUiComponent } from './component/register/register.component';
import { LoginUiComponent } from './component/login/login.component';
import { HelloPageComponent } from './component/hello-page/hello-page.component';
import { UserPostsPageComponent } from './component/user-posts-page/user-posts-page.component';
import { AuthGuard } from './shared/auth.guard';


export const routes: Routes = [
  { path: '', component: HelloPageComponent },
  { path: 'register', component: RegisterUiComponent },
  { path: 'login', component: LoginUiComponent },
  { path: 'user/posts', component: UserPostsPageComponent, canActivate: [AuthGuard]}
];
