import { Component, inject, OnInit } from '@angular/core';
import { AddButtonComponent } from "../../ui/add-button/add-button.component";
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { DashServerModule } from '../../modules/dash-server/dash-server.module';
import { DashServerDisplay, Display } from '../../modules/dash-server/dash-server';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { DisplaySettingsComponent } from '../display-settings/display-settings.component';
import { DashComponent } from '../dash/dash.component';
import { ButtonCpClipboardComponent } from "../../ui/button-cp-clipboard/button-cp-clipboard.component";

@Component({
  selector: 'app-edit',
  standalone: true,
  imports: [
    AddButtonComponent,
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    DashServerModule,
    MatSnackBarModule,
    MatDialogModule,
    DashComponent,
    ButtonCpClipboardComponent
  ],
  templateUrl: './edit.component.html',
  styleUrl: './edit.component.sass'
})
export class EditComponent implements OnInit {
  private readonly defaultDisplayId = '5e02c38b-fff2-4991-85dc-693aa2a82b6d';
  private _snackBar = inject(MatSnackBar);
  readonly dialog = inject(MatDialog);
  displayData?: Display;
  shareUrl = '';

  constructor(private dashAPI: DashServerDisplay) { }

  ngOnInit(): void {
    this.load();
  }

  options(): void {
    const dialogRef = this.dialog.open(DisplaySettingsComponent, {
      disableClose: true,
      data: this.displayData,
    });

    dialogRef.afterClosed().subscribe(result => {
      if (!result) {
        this.load();
        return;
      }

      this.dashAPI.update(result).subscribe(_ => {
        this.load();
        this.refresh(false);
        this._snackBar.open('Display settings updated.');
      });
    });
  }

  refresh(showMessage = true): void {
    this.dashAPI.refresh(this.defaultDisplayId).subscribe(_ => {
      if (showMessage) {
        this._snackBar.open('Display refreshed.');
      }
    });
  }

  load(): void {
    this.dashAPI.edit().subscribe(response => {
      this.displayData = response.result;
      this.shareUrl = `${window.location.origin}/#/display/${this.displayData?.shareId}`;
    });
  }
}
