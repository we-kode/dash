import { Component, inject, OnInit } from '@angular/core';
import { AddButtonComponent } from "../../ui/add-button/add-button.component";
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { PinboardEditComponent } from '../edit/edit.component';
import { DashServerPinboard, Information } from '../../modules/dash-server/dash-server';
import { MatListModule, MatListOption } from '@angular/material/list';
import { MatDividerModule } from '@angular/material/divider';
import { DashServerModule } from '../../modules/dash-server/dash-server.module';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { ConfirmDialogComponent } from '../../ui/confirm-dialog/confirm-dialog.component';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { NoElementsComponent } from '../../ui/no-elements/no-elements.component';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

@Component({
  selector: 'app-information-overview',
  standalone: true,
  imports: [
    AddButtonComponent,
    MatDialogModule,
    MatListModule,
    MatDividerModule,
    DashServerModule,
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    MatSnackBarModule,
    NoElementsComponent,
    MatProgressSpinnerModule
  ],
  templateUrl: './overview.component.html',
  styleUrl: './overview.component.sass'
})
export class OverviewComponent implements OnInit {
  readonly dialog = inject(MatDialog);
  private _snackBar = inject(MatSnackBar);
  isEditMode = false;
  isLoading = false;
  items: Information[] = [];

  constructor(private dashAPI: DashServerPinboard) { }

  ngOnInit(): void {
    this.reload();
  }

  add(): void {
    this.dashAPI.newId().subscribe(response =>
      this.openDialog(new Information({
        id: response.result,
        name: ''
      }))
    );
  }

  edit(item: Information) {
    this.openDialog(item);
  }

  private openDialog(elem: Information): void {
    const dialogRef = this.dialog.open(PinboardEditComponent, {
      disableClose: true,
      data: elem,
    });

    dialogRef.afterClosed().subscribe(result => {
      if (!result) {
        return;
      }

      this.dashAPI.save(result).subscribe(_ => {
        this.reload();
        this._snackBar.open('Item saved.');
      },);
    });
  }

  delete(items: MatListOption[]) {
    if (items.length === 0) {
      this.resetEditMode();
      return;
    }

    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      disableClose: true,
    });

    dialogRef.afterClosed().subscribe(result => {
      if (!result) {
        this.resetEditMode();
        return;
      }

      this.dashAPI.delete(items.map(item => (item.value as Information).id!)).subscribe(_ => {
        this.resetEditMode();
        this.reload();
        this._snackBar.open('Items removed.');
      });
    });
  }

  private resetEditMode(): void {
    this.isEditMode = !this.isEditMode;
  }

  reload(): void {
    this.isLoading = true;
    this.dashAPI.listInfoItems().subscribe(response => {
      this.items = response.result;
      this.isLoading = false;
    });
  }
}
