import { Component, Input } from '@angular/core';
import { Element } from '../../modules/dash-server/dash-server';

@Component({
  selector: 'app-image',
  standalone: true,
  imports: [],
  templateUrl: './image.component.html',
  styleUrl: './image.component.sass'
})
export class ImageComponent {
  @Input()
  element!: Element;

  config = {
    width: undefined,
    height: undefined
  }
}
