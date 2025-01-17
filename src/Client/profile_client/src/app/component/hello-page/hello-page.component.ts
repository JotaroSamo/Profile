import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';



@Component({
  selector: 'app-hello-page',
  imports: [RouterLink],
  templateUrl: './hello-page.component.html',
  styleUrl: './hello-page.component.scss'
})
export class HelloPageComponent {
}
