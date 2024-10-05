import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Element } from '../../modules/dash-server/dash-server';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { OverviewComponent } from "../../calendar/overview/overview.component";
import { TextComponent } from '../../text/text.component';
import { DateComponent } from '../../date/date.component';
import { ClockComponent } from '../../clock/clock.component';
import { CalendarComponent } from '../../calendar/calendar/calendar.component';
import { ImageComponent } from '../../image/image/image.component';
import { PinboardComponent } from '../../pinboard/pinboard/pinboard.component';

@Component({
  selector: 'app-element',
  standalone: true,
  imports: [
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    OverviewComponent,
    TextComponent,
    DateComponent,
    ClockComponent,
    CalendarComponent,
    ImageComponent,
    PinboardComponent
  ],
  templateUrl: './element.component.html',
  styleUrl: './element.component.sass'
})
export class ElementComponent {
  @Input()
  element!: Element;

  @Input()
  cIdentifier!: string;

  @Input()
  isEditMode = false;

  @Output()
  remove: EventEmitter<Element> = new EventEmitter();

  showActions = false;

  delete(): void {
    this.remove.emit(this.element);
  }

  settings(): void {

  }
}
