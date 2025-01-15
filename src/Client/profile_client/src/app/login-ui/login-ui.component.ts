// login.component.ts
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../data/services/user-service.service';
import { LoginUser } from '../data/interface/auth/LoginUser';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login-ui.component.html',
  styleUrls: ['./login-ui.component.scss']
})
export class LoginComponent {
  loginForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private router: Router
  ) {
    this.loginForm = this.fb.group({
      login: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  onSubmit(): void {
    if (this.loginForm.valid) {
      const loginUser: LoginUser = this.loginForm.value;
      this.userService.login(loginUser).subscribe(
        response => {
          // Сохранение токена и перенаправление
          localStorage.setItem('authToken', response.token);
          this.router.navigate(['/']);
        },
        error => {
          console.error('Login failed', error);
        }
      );
    }
  }
}

