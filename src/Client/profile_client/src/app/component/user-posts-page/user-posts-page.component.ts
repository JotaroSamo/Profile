import { Component, OnInit } from '@angular/core';
import { PostService } from '../../data/services/post.service';
import { UserPosts } from '../../data/interface/user/UserPosts';
import { AuthService } from '../../data/services/auth.service';
import { Router } from '@angular/router';

import { CommonModule, DatePipe } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button'; 
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-user-posts-page',
  imports: [CommonModule, ReactiveFormsModule, DatePipe, MatButtonModule, MatCardModule, MatChipsModule, MatIconModule],
  templateUrl: './user-posts-page.component.html',
  styleUrls: ['./user-posts-page.component.scss'],
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

  deletePost(id: string): void {
    if (id != null) {
      this.postService.deletePost(id).subscribe(
        (response) => {
          if (response === true) {
            this.ngOnInit(); 
          }
        },
        (error) => {
          console.error('Ошибка при удалении поста:', error); 
        }
      );
    }
  }
}


