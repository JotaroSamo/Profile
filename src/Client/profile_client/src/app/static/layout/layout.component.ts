import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { SidebarComponent } from '../sidebar/sidebar.component';
import {MatGridListModule} from '@angular/material/grid-list';



@Component({
  selector: 'app-layout',
  imports: [RouterOutlet, SidebarComponent, MatGridListModule],
  templateUrl: './layout.component.html',
  styleUrl: './layout.component.scss'
})
export class LayoutComponent {





}
