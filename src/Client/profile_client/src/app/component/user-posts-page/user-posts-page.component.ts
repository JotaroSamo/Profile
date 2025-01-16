import { Component, OnInit } from '@angular/core';
import { PostService } from '../../data/services/post.service';
import { UserPosts } from '../../data/interface/user/UserPosts';
import { HeaderUiComponent } from "../../static/header/header.component";
import { FooterUiComponent } from "../../static/footer/footer.component";
import { AuthService } from '../../data/services/auth.service';
import { Router } from '@angular/router';
import { SidebarComponent } from "../sidebar/sidebar.component";

@Component({
  selector: 'app-user-posts-page',
  templateUrl: './user-posts-page.component.html',
  styleUrls: ['./user-posts-page.component.scss'],
  imports: [HeaderUiComponent, FooterUiComponent, SidebarComponent]
})
export class UserPostsPageComponent implements OnInit {
  userPosts: UserPosts | undefined;
  constructor(private postService: PostService, private authService: AuthService, private router: Router) {}

  ngOnInit(): void {
    this.postService.getUserPost().subscribe((data: UserPosts) => {
      this.userPosts = data;
    },
    (error) => { 
      console.error('Ошибка:', error); 
  });
  }
  logout(): void { 
    this.authService.logout();
    this.router.navigate(['']);
    
  };
}


