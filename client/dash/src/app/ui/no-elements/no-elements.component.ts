import { Component, EventEmitter, Output } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'ui-no-elements',
  standalone: true,
  imports: [
    MatIconModule,
    MatButtonModule,
  ],
  templateUrl: './no-elements.component.html',
  styleUrl: './no-elements.component.sass'
})
export class NoElementsComponent {
  @Output()
  refresh: EventEmitter<void> = new EventEmitter();
}
