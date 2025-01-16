import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserPosts } from '../interface/user/UserPosts';
import { Observable } from 'rxjs';
import { environment } from '../../constant/environment';

@Injectable({
  providedIn: 'root'
})
export class PostService {

  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getUserPost(): Observable<UserPosts> {
    return this.http.get<UserPosts>(`${this.apiUrl}post/user-posts`);
  }
}
