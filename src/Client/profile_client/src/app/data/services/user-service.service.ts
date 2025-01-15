// user.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../constant/environment';
import { CreateUser } from '../interface/user/CreateUser';
import { LoginUser } from '../interface/auth/LoginUser';


@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  register(user: CreateUser): Observable<any> {
    return this.http.post(`${this.apiUrl}/register`, user);
  }

  login(user: LoginUser): Observable<any> {
    return this.http.post(`${this.apiUrl}/login`, user);
  }
}
