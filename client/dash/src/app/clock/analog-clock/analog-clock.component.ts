import { AfterViewInit, Component, ElementRef, Input, NgZone, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { Subscription, tap, timer } from 'rxjs';

@Component({
  selector: 'app-analog-clock',
  standalone: true,
  imports: [
  ],
  templateUrl: './analog-clock.component.html',
  styleUrl: './analog-clock.component.sass'
})
export class AnalogClockComponent implements OnInit, OnDestroy, AfterViewInit {
  @ViewChild('mycanvas', { static: false, read: ElementRef }) canvasRef!: ElementRef;
  @Input() size = 100;
  private canvasContext!: CanvasRenderingContext2D;
  private time = new Date();
  subscription!: Subscription;

  constructor(private ngZone: NgZone) {}

  ngOnInit() {}
 
 
  ngAfterViewInit() {
    const canvasEl: HTMLCanvasElement = this.canvasRef.nativeElement;
    canvasEl.width = this.size;
    canvasEl.height = this.size;
    const radius = canvasEl.height / 2;
    const innerRadius = radius * 0.89;
    this.canvasContext = canvasEl.getContext('2d')!;
    this.canvasContext.translate(radius, radius);
    this.ngZone.runOutsideAngular(() => this.draw(innerRadius));
  }
  ngOnChanges(change: any) {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
    if (change.tdate) {
      this.time = change.tdate.currentValue;
      if (this.canvasRef) {
        const canvasEl: HTMLCanvasElement = this.canvasRef.nativeElement;
        canvasEl.width = this.size;
        canvasEl.height = this.size;
        const radius = canvasEl.height / 2;
        const innerRadius = radius * 0.89;
        this.canvasContext = canvasEl.getContext('2d')!;
        this.canvasContext.translate(radius, radius);
        this.ngZone.runOutsideAngular(() => this.draw(innerRadius));
      }
    }
  }

  ngOnDestroy() {}

  draw(innerRadius: number) {
    this.drawFace(this.canvasContext, innerRadius);
    let date = new Date(this.time);
    this.subscription = timer(0, 1000)
      .pipe(
        tap(t => {
          this.canvasContext.clearRect(this.size / 2 * -1, this.size / 2 * -1, this.size, this.size);
          date = new Date(date.setSeconds(date.getSeconds() + 1));
          this.drawFace(this.canvasContext, innerRadius);
          this.drawNumbers(this.canvasContext, innerRadius);
          this.drawTime(this.canvasContext, innerRadius, date);
        })
      )
      .subscribe(s => {
      }
      );
  }

  drawFace(ctx: any, radius: any) {
    ctx.beginPath();
    ctx.arc(0, 0, radius, 0, 2 * Math.PI);
    ctx.fillStyle = 'transparent';
    ctx.fill();
    const grad = ctx.createRadialGradient(0, 0, radius, 0.95, 0, 0, radius, 1.05);
    ctx.strokeStyle = grad;
    ctx.lineWidth = radius * 0.025;
    ctx.stroke();
    ctx.beginPath();
    ctx.arc(0, 0, radius, 0.1, 0, 2, Math.PI);
    ctx.fillStyle = '#e1e2e9';
    ctx.fill();
  }

  drawNumbers(ctx: any, radius: any) {
    let ang;
    let num;
    let dot = '.';
    ctx.font = radius * 0.1 + 'pt Courier';
    ctx.textBaseline = 'middle';
    ctx.textAlign = 'center';
    for (num = 1; num < 13; num++) {
      ang = num * Math.PI / 6;
      ctx.rotate(ang);
      ctx.translate(0, -radius * 0.85);
      ctx.rotate(-ang);
      if (num === 3 || num === 6 || num === 9 || num == 12) {
        ctx.fillText(num, 0, 0);
      } else {
        ctx.fillText(dot, 0, 0);
      }
      ctx.rotate(ang);
      ctx.translate(0, radius * 0.85);
      ctx.rotate(-ang);
    }
  }

  drawTime(ctx: any, radius: any, date: Date) {
    this.drawHourHand(ctx, date, radius * 0.6, radius * 0.05);
    this.drawMinuteHand(ctx, date, radius * 0.8, radius * 0.04);
    this.drawSecondHand(ctx, date, radius * 0.9, radius * 0.03);
  }

  degreesToRadians(degrees: any) {
    return (Math.PI / 180) * degrees
  }

  drawHourHand(ctx: any, theDate: any, length: any, width: any) {
    var hours = theDate.getHours() + theDate.getMinutes() / 60;
    var degrees = (hours * 360 / 12) % 360;
    ctx.save();
    ctx.strokeStyle = '#424751';
    ctx.rotate(this.degreesToRadians(degrees));
    this.drawHand(ctx, length, width, 3);
    ctx.restore();
  }

  drawMinuteHand(ctx: any, theDate: any, length: any, width: any) {
    var minutes = theDate.getMinutes() + theDate.getSeconds() / 60;
    ctx.save();
    ctx.strokeStyle = '#424751';
    ctx.rotate(this.degreesToRadians(minutes * 6));
    this.drawHand(ctx, length, width, 5);
    ctx.restore();
  }

  drawHand(ctx: any, size: any, thickness: any, shadowOffset: any) {
    thickness = thickness || 4;
    ctx.beginPath();
    ctx.moveTo(0, 0); // center 
    ctx.lineTo(thickness * -1, -10);
    ctx.lineTo(0, size * -1);
    ctx.lineTo(thickness, -10);
    ctx.lineTo(0, 0);
    ctx.fill();
    ctx.stroke();
  }

  drawSecondHand(ctx: any, theDate: any, length: any, width: any) {
    var seconds = theDate.getSeconds();
    ctx.save();
    ctx.fillStyle = '#d1ffa1';
    ctx.strokeStyle = "#d1ffa1";
    ctx.globalAlpha = 0.8;
    ctx.rotate(this.degreesToRadians(seconds * 6));
    this.drawHand(ctx, length, width, 0);
    ctx.restore();
  }
}
