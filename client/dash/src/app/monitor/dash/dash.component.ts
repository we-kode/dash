import { Component, inject, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { DisplayGrid, GridsterConfig, GridsterItem, GridsterItemComponentInterface, GridsterModule, GridType } from 'angular-gridster2';
import { DashServerElement, Display, Element, Component as DComponent, ElementRequest } from '../../modules/dash-server/dash-server';
import { AddButtonComponent } from '../../ui/add-button/add-button.component';
import { MatDialog } from '@angular/material/dialog';
import { ComponentListComponent } from '../component-list/component-list.component';
import { ElementComponent } from '../element/element.component';
import { ConfirmDialogComponent } from '../../ui/confirm-dialog/confirm-dialog.component';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-dash',
  standalone: true,
  imports: [
    GridsterModule,
    AddButtonComponent,
    ElementComponent,
    MatIconModule,
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
    draggable: {
      enabled: false
    },
    resizable: {
      enabled: false
    },
  };
  dashboard: Array<GridsterItem> = [];

  constructor(private dashApi: DashServerElement) {

  }

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
    this.options!.draggable!.enabled = this.isEditMode;
    this.options!.draggable!.stop = this.dragStopped.bind(this);
    this.options!.resizable!.enabled = this.isEditMode;
    this.options!.resizable!.stop = this.resizeStopped.bind(this);

    if (this.displayData?.displayId) {
      this.dashApi.getByDisplay(this.displayData?.displayId!).subscribe(response => {
        this.dashboard = response.result.map((e: Element) => {
          return <GridsterItem>{
            cols: e.cols,
            rows: e.rows,
            x: e.x,
            y: e.y,
            element: e,
            cIdentifier: e.component?.identifier
          }
        });
      });
    }

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

      const r = result as DComponent;
      let newElement: ElementRequest | any = new ElementRequest(
        {
          componentId: r.id!,
          config: '{}',
          displayId: this.displayData?.displayId!,
          x: 0,
          y: 0,
          cols: 1,
          rows: 1
        }
      );

      this.dashApi.create(
        newElement,
      ).subscribe(reponse => {
        newElement['elementId'] = reponse.result;
        this.dashboard.push({ x: newElement.x!, y: newElement.y!, cols: newElement.cols!, rows: newElement.rows!, element: newElement, cIdentifer: r.identifier });
      });
    });
  }

  remove(elem: Element): void {

    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      disableClose: true,
    });

    dialogRef.afterClosed().subscribe(result => {
      if (!result) {
        return;
      }

      this.dashApi.delete(elem.elementId!, elem.displayId).subscribe(_ => {
        this.dashboard = this.dashboard.filter(item => item['element'].elementId !== elem.elementId);
      });
    });
  }

  update(elem: Element): void {
    this.dashApi.update(elem.elementId!, elem).subscribe();
  }

  resizeStopped(
    item: GridsterItem,
    itemComponent: GridsterItemComponentInterface,
    event: MouseEvent
  ): void {
    this.updateElement((item['element'] as Element), itemComponent);
  }

  dragStopped(
    item: GridsterItem,
    itemComponent: GridsterItemComponentInterface,
    event: MouseEvent
  ): void {
    if (item.x === itemComponent.$item.x && item.y === itemComponent.$item.y) {
      return;
    }
    this.updateElement((item['element'] as Element), itemComponent);
  }

  private updateElement(elem: Element, itemComponent: GridsterItemComponentInterface): void {
    elem.x = itemComponent.$item.x;
    elem.y = itemComponent.$item.y;
    elem.cols = itemComponent.$item.cols;
    elem.rows = itemComponent.$item.rows;
    this.update(elem);
  }
}
