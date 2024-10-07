import { Component, OnDestroy, OnInit } from '@angular/core';
import { Ng2FittextModule } from 'ng2-fittext';

@Component({
  selector: 'app-digital-clock',
  standalone: true,
  imports: [
    Ng2FittextModule
  ],
  templateUrl: './digital-clock.component.html',
  styleUrl: './digital-clock.component.sass'
})
export class DigitalClockComponent implements OnInit, OnDestroy {
  time = new Date();
  private intervalId: any;

  ngOnInit(): void {
    this.intervalId = setInterval(() => {
      this.time = new Date();
    }, 1000);
  }

  ngOnDestroy() {
    clearInterval(this.intervalId);
  }
}
