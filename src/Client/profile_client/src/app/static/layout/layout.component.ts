import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { SidebarComponent } from '../sidebar/sidebar.component';
import { MatGridListModule } from '@angular/material/grid-list';
import { AuthService } from '../../data/services/auth.service';
;
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-layout',
  imports: [RouterOutlet, SidebarComponent, MatGridListModule, CommonModule],
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.scss']
})
export class LayoutComponent implements OnInit {
  isLoading = true;

  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit(): void {


      if (this.authService.isAuthenticated()) {
       this.router.navigate(['/user/posts'])
      }
      else
      {
        this.router.navigate([''])
      }

  }
}

