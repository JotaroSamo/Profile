import {
    HttpEvent,
    HttpHandler,
    HttpInterceptor,
    HttpRequest,
  } from '@angular/common/http';
  import { Injectable } from '@angular/core';
  import { Observable, throwError } from 'rxjs';
  import { AuthService } from '../data/services/auth.service';
  import { Router } from '@angular/router';


  
  @Injectable()
  export class AuthInterceptor implements HttpInterceptor {
    constructor(private authService: AuthService,
                private router: Router,
                ) { }
                
                intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
                   
                    const token = this.authService.token;
                    console.log('Токен:', token);
                    
                    if (token) {
                      const clonedRequest = req.clone({
                        setHeaders: {
                          Authorization: `Bearer ${token}`
                        }
                      });
                      return next.handle(clonedRequest);
                    }
                
        
                    return next.handle(req);
                  }
     
    }
  