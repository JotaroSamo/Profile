
import { Routes } from '@angular/router';
import { RegisterUiComponent } from './component/register/register.component';
import { LoginUiComponent } from './component/login/login.component';
import { HelloPageComponent } from './component/hello-page/hello-page.component';
import { UserPostsPageComponent } from './component/user-posts-page/user-posts-page.component';
import { AuthGuard } from './shared/auth.guard';
import { CreatePostComponent } from './component/create-post/create-post.component';
import { LayoutComponent } from './static/layout/layout.component';


export const routes: Routes = [
  { path: '', component: LayoutComponent, children : 
    [
      {path: '', component: HelloPageComponent},
      {path: 'user/posts', component: UserPostsPageComponent, canActivate: [AuthGuard]},
      {path : 'user/posts/create', component: CreatePostComponent, canActivate: [AuthGuard]},
      { path: 'register', component: RegisterUiComponent },
      { path: 'login', component: LoginUiComponent },
  
    ] },
  

];
