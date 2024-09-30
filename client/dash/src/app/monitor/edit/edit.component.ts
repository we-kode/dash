import { Component, inject } from '@angular/core';
import { AddButtonComponent } from "../../ui/add-button/add-button.component";
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { DashServerModule } from '../../modules/dash-server/dash-server.module';
import { DashServerDisplay } from '../../modules/dash-server/dash-server';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';

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
  ],
  templateUrl: './edit.component.html',
  styleUrl: './edit.component.sass'
})
export class EditComponent {
  private readonly defaultDisplayId = '5e02c38b-fff2-4991-85dc-693aa2a82b6d';
  private _snackBar = inject(MatSnackBar);

  constructor(private dashAPI: DashServerDisplay) { }

  add(): void {

  }

  options(): void {

  }

  refresh(): void {
    this.dashAPI.refresh(this.defaultDisplayId).subscribe(_ => {
      this._snackBar.open('Display refreshed.');
    });
  }
}
