import { Component } from '@angular/core';

@Component({
  selector: 'app-display',
  standalone: true,
  imports: [],
  templateUrl: './display.component.html',
  styleUrl: './display.component.sass'
})
export class DisplayComponent {


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
