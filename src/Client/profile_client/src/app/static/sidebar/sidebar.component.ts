import { Component } from '@angular/core';
import { AuthService } from '../../data/services/auth.service';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-sidebar',
  imports: [RouterLink],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.scss'
})
export class SidebarComponent {
  
  constructor(private authService: AuthService, private roter: Router)
  { }
  IsAuth(): boolean
  {
    return this.authService.isAuthenticated();
  }
  logout()
  {
    this.authService.logout();
    this.roter.navigate(['']);

  }

}
