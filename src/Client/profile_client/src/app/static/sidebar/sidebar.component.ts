import { Component } from '@angular/core';
import { AuthService } from '../../data/services/auth.service';
import { Router, RouterLink } from '@angular/router';
import { SearchBarComponent } from "../search-bar/search-bar.component";

@Component({
  selector: 'app-sidebar',
  imports: [RouterLink, SearchBarComponent],
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
