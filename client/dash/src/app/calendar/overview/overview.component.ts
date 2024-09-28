import { Component } from '@angular/core';
import { AddButtonComponent } from '../../ui/add-button/add-button.component';

@Component({
  selector: 'app-calendar-overview',
  standalone: true,
  imports: [
    AddButtonComponent
  ],
  templateUrl: './overview.component.html',
  styleUrl: './overview.component.sass'
})
export class OverviewComponent {
  add(): void { }
}
