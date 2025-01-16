import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { LoadingService } from './data/services/loadin.service';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'profile_client';
  constructor(private loadingService: LoadingService) {
      
  }

  ngOnInit(): void {
    this.loadingService.behavior.subscribe((loadingStatus) => {
      console.log('Loading status:', loadingStatus);
    });
  }
  
}
