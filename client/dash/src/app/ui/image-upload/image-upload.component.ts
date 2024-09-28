import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormControl, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'ui-image-upload',
  standalone: true,
  imports: [
    CommonModule,
    MatFormFieldModule,
    MatIconModule,
    MatButtonModule,
    MatInputModule
  ],
  templateUrl: './image-upload.component.html',
  styleUrl: './image-upload.component.sass'
})
export class ImageUploadComponent {
  @Output()
  imageChanged: EventEmitter<string | undefined> = new EventEmitter();

  @Input()
  selectedImage? = '';

  setFileData(event: Event): void {
    const eventTarget: HTMLInputElement | null = event.target as HTMLInputElement | null;
    if (eventTarget?.files?.[0]) {
      const file: File = eventTarget.files[0];
      const reader = new FileReader();
      reader.addEventListener('load', () => {
        console.log(reader.result);
        this.selectedImage = reader.result as string;
        this.imageChanged.emit(this.selectedImage);
      });
      reader.readAsDataURL(file);
    }
  }

  remove(): void {
    this.selectedImage = undefined;
    this.imageChanged.emit(this.selectedImage);
  }
}
