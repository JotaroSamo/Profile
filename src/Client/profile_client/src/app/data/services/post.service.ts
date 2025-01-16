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
    const token = localStorage.getItem('token'); // Получение токена из Local Storage
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`, // Добавление заголовка Authorization
    });

    return this.http.get<UserPosts>(`${this.apiUrl}post/user-posts`, { headers });
  }
}
