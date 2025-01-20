import { Component } from '@angular/core';
import { BaseUser } from '../../data/interface/user/BaseUser';
import { UserService } from '../../data/services/user.service';
import { MatButtonModule } from '@angular/material/button'; 
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';
import { ReactiveFormsModule } from '@angular/forms';

import { catchError } from 'rxjs/operators';
import { of } from 'rxjs';
import { NgFor, CommonModule } from '@angular/common';
@Component({
  selector: 'app-search-bar',
  imports: [CommonModule,MatButtonModule, MatCardModule, MatChipsModule, MatIconModule, MatToolbarModule, NgFor],
  templateUrl: './search-bar.component.html',
  styleUrls: ['./search-bar.component.scss']
})
export class SearchBarComponent {

  constructor(private userService: UserService) {}
  users: BaseUser[] = [];
  errorMessage: string = '';

  onSearchChange(event: Event): void {
    const inputElement = event.target as HTMLInputElement;
    const searchValue = inputElement?.value;

    if (searchValue) {
      this.userService.findUsers(searchValue).pipe(
        catchError(error => {
          this.errorMessage = 'Произошла ошибка при поиске пользователей';
          return of([]);
        })
      ).subscribe((data: BaseUser[]) => {
        this.users = data;
        this.errorMessage = '';
      });
    } else {
      this.users = [];
      this.errorMessage = '';
    }
  }
}
