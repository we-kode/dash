import { Component } from '@angular/core';
import { AddButtonComponent } from "../../ui/add-button/add-button.component";

@Component({
  selector: 'app-edit',
  standalone: true,
  imports: [AddButtonComponent],
  templateUrl: './edit.component.html',
  styleUrl: './edit.component.sass'
})
export class EditComponent {

  add(): void {

  }
}
