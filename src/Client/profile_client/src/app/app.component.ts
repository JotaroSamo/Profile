import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderUiComponent } from "./static/header-ui/header-ui.component";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, HeaderUiComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'profile_client';

  
}
