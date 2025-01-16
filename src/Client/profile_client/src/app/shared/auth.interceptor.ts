import {
  HttpErrorResponse,
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
  HttpResponse
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { AuthService } from '../data/services/auth.service';
import { Router } from '@angular/router';
import { LoadingService } from '../data/services/loadin.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(
    private authService: AuthService,
    private router: Router,
    private loadingService: LoadingService
  ) {}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    console.log("AuthInterceptor works!!!");
    
    // Установка состояния загрузки по началу выполнения запроса
    this.loadingService.setLoading(true, req.url);

    req = req.clone({
      setHeaders: {
        Authorization: `Bearer ${this.authService.token}`,
      },
    });

    return next.handle(req).pipe(
      // Управление ошибками
      catchError((error: HttpErrorResponse) => {
        this.loadingService.setLoading(false, req.url);
        
        if ([401, 403].indexOf(error.status) !== -1) {
          this.authService.logout();
          this.router.navigate(['/login'], {
            queryParams: { authFailed: true }
          }).then(() => {});
        }
        
        if (error.status === 400 && error.error) {
          const errors = [];
          if (Array.isArray(error.error)) {
            for (const errs of error.error) {
              errors.push(...errs.errors);
            }
          }
        }
        
        return throwError(error);
      }),
      // Обработка успешного ответа
      map((evt: HttpEvent<any>) => {
        if (evt instanceof HttpResponse) {
          this.loadingService.setLoading(false, req.url);
        }
        return evt;
      })
    );
  }
}

