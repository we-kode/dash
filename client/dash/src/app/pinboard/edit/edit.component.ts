import { Component, inject, signal } from '@angular/core';
import { Information } from '../../modules/dash-server/dash-server';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { provideNativeDateAdapter } from '@angular/material/core';
import { MatSlideToggleChange, MatSlideToggleModule } from '@angular/material/slide-toggle';
import { FormControl, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { merge } from 'rxjs';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { ImageUploadComponent } from "../../ui/image-upload/image-upload.component";
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-pinnboard-edit',
  standalone: true,
  providers: [provideNativeDateAdapter()],
  imports: [
    MatDialogModule,
    MatButtonModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,
    MatSlideToggleModule,
    ReactiveFormsModule,
    FormsModule,
    ImageUploadComponent,
  ],
  templateUrl: './edit.component.html',
  styleUrl: './edit.component.sass'
})
export class PinboardEditComponent {
  readonly title = new FormControl('', [Validators.required]);
  readonly dialogRef = inject(MatDialogRef<PinboardEditComponent>);
  errorMessage = signal('');
  information: Information;

  constructor() {
    this.information = inject(MAT_DIALOG_DATA);

    merge(this.title.statusChanges, this.title.valueChanges)
      .pipe(takeUntilDestroyed())
      .subscribe(() => this.updateErrorMessage());
  }

  updateErrorMessage(): void {
    if (this.title.hasError('required')) {
      this.errorMessage.set('You must enter a value');
    } else {
      this.errorMessage.set('');
    }
  }

  updateFocus(e: MatSlideToggleChange): void {
    this.information.isFocused = e.checked;
  }

  updateImage(image: string | undefined) {
    this.information.image = image;
  }

  save(): void {
    if (this.title.invalid) {
      return;
    }

    this.dialogRef.close(this.information);
  }
}
