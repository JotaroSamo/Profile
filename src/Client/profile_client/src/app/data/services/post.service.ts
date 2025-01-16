import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserPosts } from '../interface/user/UserPosts';
import { Observable } from 'rxjs';
import { environment } from '../../constant/environment';
import { CreatePost } from '../interface/post/CreatePost';
import { BasePost } from '../interface/post/BasePost';

@Injectable({
  providedIn: 'root'
})
export class PostService {

  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getUserPost(): Observable<UserPosts> {
    return this.http.get<UserPosts>(`${this.apiUrl}post/user-posts`);
  }
  createPost(post: CreatePost): Observable<BasePost> {
    return this.http.post<BasePost>(`${this.apiUrl}post/create`, post);
  }
}
