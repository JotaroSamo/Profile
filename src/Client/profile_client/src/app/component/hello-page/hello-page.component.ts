import { Component } from '@angular/core';
import { HeaderUiComponent } from "../../static/header/header.component";
import { FooterUiComponent } from "../../static/footer/footer.component";
import { MainUiComponent } from "../main/main.component";

@Component({
  selector: 'app-hello-page',
  imports: [HeaderUiComponent, FooterUiComponent, MainUiComponent],
  templateUrl: './hello-page.component.html',
  styleUrl: './hello-page.component.scss'
})
export class HelloPageComponent {

}
