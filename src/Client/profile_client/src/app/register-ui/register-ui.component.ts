// register.component.ts
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserService} from '../data/services/user-service.service';
import { CreateUser } from '../data/interface/user/CreateUser';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register-ui.component.html',
  styleUrls: ['./register-ui.component.scss']
})
export class RegisterComponent {
  registerForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private router: Router
  ) {
    this.registerForm = this.fb.group({
      login: ['', Validators.required],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      avatarUrl: [''],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', [Validators.required, Validators.minLength(6)]]
    }, { validator: this.passwordMatchValidator });
  }

  passwordMatchValidator(form: FormGroup) {
    return form.get('password')!.value === form.get('confirmPassword')!.value
      ? null : { mismatch: true };
  }

  onSubmit(): void {
    if (this.registerForm.valid) {
      const createUser: CreateUser = this.registerForm.value;
      this.userService.register(createUser).subscribe(
        response => {
          // Перенаправление после успешной регистрации
          this.router.navigate(['/login']);
        },
        error => {
          console.error('Registration failed', error);
        }
      );
    }
  }
}
