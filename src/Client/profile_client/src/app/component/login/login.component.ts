// login-ui.component.ts
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule, FormControl, FormGroupDirective, NgForm } from '@angular/forms';

import { LoginUser } from '../../data/interface/auth/LoginUser'; 
import { Router } from '@angular/router';
import { AuthService } from '../../data/services/auth.service';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { ErrorStateMatcher } from '@angular/material/core';
export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}
@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, MatInputModule, MatButtonModule, MatCardModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginUiComponent {
  loginForm: FormGroup;
  matcher = new MyErrorStateMatcher();
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
