import { Component, inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { Display } from '../../modules/dash-server/dash-server';
import { FormsModule } from '@angular/forms';
import { MatTooltipModule } from '@angular/material/tooltip';
import { ButtonCpClipboardComponent } from "../../ui/button-cp-clipboard/button-cp-clipboard.component";

@Component({
  selector: 'app-display-settings',
  standalone: true,
  imports: [
    MatDialogModule,
    MatButtonModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    FormsModule,
    MatTooltipModule,
    ButtonCpClipboardComponent
],
  templateUrl: './display-settings.component.html',
  styleUrl: './display-settings.component.sass'
})
export class DisplaySettingsComponent {
  readonly dialogRef = inject(MatDialogRef<DisplaySettingsComponent>);
  readonly minValue = 1;
  readonly maxValue = 2147483647;

  private baseUrl = window.location.origin;
  data: Display;
  shareUrl: string;

  constructor() {
    this.data = inject(MAT_DIALOG_DATA);
    this.shareUrl = `${this.baseUrl}/#/display/${this.data.shareId}`;
  }

  save(): void {
    this.data.columns = this.validateNumbers(this.data.columns);
    this.data.rows = this.validateNumbers(this.data.rows);
    this.dialogRef.close(this.data);
  }

  isNumber(event: KeyboardEvent): boolean {
    return /^[0-9]$/i.test(event.key);
  }

  private validateNumbers(value: number): number {
    if (value < this.minValue) {
      value = this.minValue;
    } else if (value > this.maxValue) {
      value = this.maxValue;
    }

    return value;
  }
}
