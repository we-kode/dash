import { Component, EventEmitter, Output } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'ui-add-button',
  standalone: true,
  imports: [
    MatButtonModule,
    MatIconModule,
  ],
  templateUrl: './add-button.component.html',
  styleUrl: './add-button.component.sass'
})
export class AddButtonComponent {
  @Output() clickedEvent = new EventEmitter<void>();
}
