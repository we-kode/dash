import { Component, inject, Input } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'ui-button-cp-clipboard',
  standalone: true,
  imports: [
    MatIconModule,
    MatButtonModule,
  ],
  templateUrl: './button-cp-clipboard.component.html',
  styleUrl: './button-cp-clipboard.component.sass'
})
export class ButtonCpClipboardComponent {
  private _snackBar = inject(MatSnackBar);
  
  @Input()
  content = '';

  async share(): Promise<void> {
    await navigator.clipboard.writeText(this.content);
    this._snackBar.open('Copied to clipboard.');
  }
}
