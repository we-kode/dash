import { Component, OnDestroy, OnInit } from '@angular/core';
import { Ng2FittextModule } from 'ng2-fittext';

@Component({
  selector: 'app-date',
  standalone: true,
  imports: [
    Ng2FittextModule
  ],
  templateUrl: './date.component.html',
  styleUrl: './date.component.sass'
})
export class DateComponent implements OnInit, OnDestroy {

  time = new Date();
  private intervalId: any;

  ngOnInit() {
    // Using Basic Interval
    this.intervalId = setInterval(() => {
      this.time = new Date();
    }, 1000);
  }

  ngOnDestroy() {
    clearInterval(this.intervalId);
  }
}
