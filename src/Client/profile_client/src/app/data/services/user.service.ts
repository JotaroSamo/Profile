// user.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { isObservable, Observable } from 'rxjs';
import { environment } from '../../constant/environment';
import { CreateUser } from '../interface/user/CreateUser';
import { LoginUser } from '../interface/auth/LoginUser';
import { JwtModel } from '../interface/auth/JwtModel';
import { BaseUser } from '../interface/user/BaseUser';


@Injectable({
  providedIn: 'root'
})
export class UserService {

  
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {
  }

  register(user: CreateUser): Observable<BaseUser> {
    return this.http.post<BaseUser>(`${this.apiUrl}user/register`, user);
  }

  findUsers(query : string) : Observable<BaseUser[]>
  {
    return this.http.get<BaseUser[]>(`${this.apiUrl}user/find-users/${query}`);
  }


 
}
