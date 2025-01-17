import { Component, OnInit } from '@angular/core';
import { CreatePost } from '../../data/interface/post/CreatePost';
import { FormArray, FormBuilder, FormControl, FormGroup, FormGroupDirective, NgForm, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { PostService } from '../../data/services/post.service';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import {MatDividerModule} from '@angular/material/divider';
import { ErrorStateMatcher } from '@angular/material/core';
export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}
@Component({
  selector: 'app-create-post',
  imports: [CommonModule, ReactiveFormsModule, MatInputModule, MatButtonModule, MatCardModule, MatDividerModule, MatInputModule],
  templateUrl: './create-post.component.html',
  styleUrl: './create-post.component.scss'
})
export class CreatePostComponent implements OnInit {
  postForm: FormGroup;
  newTagControl: FormControl; // Объявляем новый контроль для тегов
  tags: string[] = []; // Массив строк для тегов
  matcher = new MyErrorStateMatcher();
  constructor(private fb: FormBuilder, private postService: PostService) {
    this.postForm = this.fb.group({
      title: ['', Validators.required],
      content: [''],
      tags: [this.tags] // Инициализация массива тегов в форме
    });
    
    this.newTagControl = new FormControl(''); // Инициализируем контроль для новых тегов
  }

  ngOnInit(): void {}

  addTag(tag: string) {
    if (tag) {
      this.tags.push(tag); // Добавление нового тега в массив
      this.postForm.get('tags')?.setValue(this.tags); // Обновление значения в форме
      this.newTagControl.setValue(''); // Сброс поля ввода тега
    }
  }
  removeTag(index: number) {
    this.tags.splice(index, 1); // Удаление тега из массива
    this.postForm.get('tags')?.setValue(this.tags); // Обновление значения в форме
  }
  createPost() {
    if (this.postForm.valid) {
      const post: CreatePost = this.postForm.value; // Получение значений формы
      
      this.postService.createPost(post).subscribe({
        next: (response) => {
          console.log('Пост успешно создан!', response);
          // Здесь можно добавить дополнительную логику, например, перенаправление на другую страницу или уведомление пользователя
          this.resetForm(); // Возможно, надо сбросить форму после успешного создания
        },
        error: (error) => {
          console.error('Ошибка при создании поста:', error);
          // Здесь можно показать уведомление пользователю о том, что произошла ошибка
        }
      });
    }
  }
  
  resetForm() {
    this.postForm.reset(); // Сбрасываем форму
    this.tags = []; // Сбрасываем массив тегов
    this.newTagControl.setValue(''); // Сбрасываем поле ввода для тегов
  }
}