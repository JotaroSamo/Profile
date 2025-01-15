// login-ui.component.ts
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { UserService } from '../../data/services/user-service.service'; 
import { LoginUser } from '../../data/interface/auth/LoginUser'; 
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './login-ui.component.html',
  styleUrls: ['./login-ui.component.scss']
})
export class LoginUiComponent {
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
          // Сохранение токена в localStorage
          localStorage.setItem('access_token', response.token);
          localStorage.setItem('expiredAt', response.expiredAt.toString());
  
          // Перенаправление после успешного входа
          this.router.navigate(['/dashboard']);
        },
        error => {
          console.error('Login failed', error);
        }
      );
    }
  }
  
}
