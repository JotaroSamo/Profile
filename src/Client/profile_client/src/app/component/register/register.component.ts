import { Component, forwardRef, Inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { UserService } from '../../data/services/user.service';
import { CreateUser } from '../../data/interface/user/CreateUser';
import { Router } from '@angular/router';
import { HeaderUiComponent } from "../../static/header/header.component";
import { FooterUiComponent } from "../../static/footer/footer.component";
import { MainUiComponent } from "../main/main.component";

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, HeaderUiComponent, FooterUiComponent, MainUiComponent],
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterUiComponent {
  registerForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    @Inject(forwardRef(() => UserService)) private userService: UserService,
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
      console.log(this.registerForm.value)
      const createUser: CreateUser = this.registerForm.value;
      console.log(createUser)
      this.userService.register(createUser).subscribe(
        response => {
          // Перенаправление после успешной регистрации
          console.log(response);
          this.router.navigate(['']);
        },
        error => {
          console.error('Registration failed', error);
        }
      );
    }
  }
}
