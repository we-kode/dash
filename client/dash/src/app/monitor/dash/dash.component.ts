import { Component, inject, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { DisplayGrid, GridsterConfig, GridsterItem, GridsterModule, GridType } from 'angular-gridster2';
import { Display } from '../../modules/dash-server/dash-server';
import { AddButtonComponent } from '../../ui/add-button/add-button.component';
import { MatDialog } from '@angular/material/dialog';
import { ComponentListComponent } from '../component-list/component-list.component';

@Component({
  selector: 'app-dash',
  standalone: true,
  imports: [
    GridsterModule,
    AddButtonComponent,
  ],
  templateUrl: './dash.component.html',
  styleUrl: './dash.component.sass'
})
export class DashComponent implements OnInit, OnChanges {
  readonly dialog = inject(MatDialog);
  @Input()
  isEditMode = false;

  @Input()
  displayData?: Display;

  options?: GridsterConfig = {
    gridType: GridType.Fit,
  };
  dashboard: Array<GridsterItem> = [];

  ngOnInit(): void { }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes.hasOwnProperty('displayData')) {
      this.updateGridOptions();
    }
  }

  private updateGridOptions() {
    this.options!.minCols = this.displayData?.columns;
    this.options!.maxCols = this.displayData?.columns;
    this.options!.minRows = this.displayData?.rows;
    this.options!.maxRows = this.displayData?.rows;
    this.options!.displayGrid = this.isEditMode ? DisplayGrid.Always : DisplayGrid.None;

    if (this.options!.api && this.options!.api.optionsChanged) {
      this.options!.api.optionsChanged();
    }
  }

  add(): void {
    const dialogRef = this.dialog.open(ComponentListComponent, {
      disableClose: true,
      data: this.displayData,
    });

    dialogRef.afterClosed().subscribe(result => {
      if (!result) {
        return;
      }

      this.dashboard.push({ x: 0, y: 0, cols: 1, rows: 1 });
    });
  }
}
