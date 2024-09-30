import { Component, HostListener, OnInit } from '@angular/core';
import { DashHubClientService } from '../../services/dash-hub-client.service';

@Component({
  selector: 'app-display',
  standalone: true,
  imports: [],
  templateUrl: './display.component.html',
  styleUrl: './display.component.sass'
})
export class DisplayComponent implements OnInit {

  // disable contextmenuu
  @HostListener('contextmenu', ['$event'])
  onRightClick(event: any) {
    event.preventDefault();
  }

  private readonly defaultDisplayId = '5e02c38b-fff2-4991-85dc-693aa2a82b6d';
  constructor(private hubService: DashHubClientService) { }

  // TODO get shareid save it in local storage and load display info by share id

  ngOnInit(): void {
    this.hubService.displayRefreshed.subscribe(_ => {
      window.location.reload();
    });

    this.hubService.connected.subscribe(isConnected => {
      if (!isConnected) {
        return;
      }
      this.hubService.subsribe(this.defaultDisplayId);
    });
  }

  // disable left click event
  stopEvent(e: MouseEvent) {
    e.stopPropagation();
  }

  // options?: GridsterConfig;
  // dashboard: Array<GridsterItem> = [];

  // ngOnInit(): void {
  //   this.options = {
  //     gridType: GridType.Fit,
  //     minCols: 4,
  //     maxCols: 4,
  //     minRows: 8,
  //     maxRows: 8,
  //   };

  //   this.dashboard = [
  //     { cols: 4, rows: 1, y: 0, x: 0 },
  //     { cols: 2, rows: 7, y: 1, x: 0 },
  //     { cols: 2, rows: 7, y: 2, x: 3 },
  //   ];
  // }
}
