// login-ui.component.ts
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';

import { LoginUser } from '../../data/interface/auth/LoginUser'; 
import { Router } from '@angular/router';
import { AuthService } from '../../data/services/auth.service';


@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginUiComponent {
  loginForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
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
      this.authService.login(loginUser).subscribe(
        response => {
          // Перенаправление после успешного входа
          this.router.navigate(['user/posts']);
        },
        error => {
          console.error('Login failed', error);
        }
      );
    }
  }
  
}
