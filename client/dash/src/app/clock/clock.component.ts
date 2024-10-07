import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { Element } from '../modules/dash-server/dash-server';
import { AnalogClockComponent } from './analog-clock/analog-clock.component';
import { DigitalClockComponent } from './digital-clock/digital-clock.component';

@Component({
  selector: 'app-clock',
  standalone: true,
  imports: [
    AnalogClockComponent,
    DigitalClockComponent,
  ],
  templateUrl: './clock.component.html',
  styleUrl: './clock.component.sass'
})
export class ClockComponent {
  @Input()
  element!: Element;

  config = {
    'type': 'analog',
    'size': 500,
  }
}
