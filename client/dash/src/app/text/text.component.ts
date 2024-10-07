import { Component, Input } from '@angular/core';
import { Element } from '../modules/dash-server/dash-server';

@Component({
  selector: 'app-text',
  standalone: true,
  imports: [],
  templateUrl: './text.component.html',
  styleUrl: './text.component.sass'
})
export class TextComponent {
  @Input()
  element!: Element;

  config = {
    'fontSize': 'normal',
    'fontWeight': 'normal',
    'fontColor': 'inherit',
    'fontStyle': 'normal'
  }
}
