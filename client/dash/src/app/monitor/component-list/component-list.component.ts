import { Component, inject, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { DashServerElement } from '../../modules/dash-server/dash-server';
import { Component as DashComponent } from '../../modules/dash-server/dash-server';

@Component({
  selector: 'app-component-list',
  standalone: true,
  imports: [
    MatListModule,
    MatButtonModule,
    MatDialogModule,
    MatIconModule
  ],
  templateUrl: './component-list.component.html',
  styleUrl: './component-list.component.sass'
})
export class ComponentListComponent implements OnInit {
  readonly dialogRef = inject(MatDialogRef<ComponentListComponent>);
  data: DashComponent[] = [];
  constructor(private dashApi: DashServerElement) { }

  ngOnInit(): void {
    this.dashApi.components().subscribe(response => {
      this.data = response.result;
    });
  }

  select(item: DashComponent): void {
    this.dialogRef.close(item);
  }
}
