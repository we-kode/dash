import { Component, HostListener, OnInit } from '@angular/core';
import { DashHubClientService } from '../../services/dash-hub-client.service';
import { DashServerModule } from '../../modules/dash-server/dash-server.module';
import { DashServerDisplay, Display } from '../../modules/dash-server/dash-server';
import { ActivatedRoute } from '@angular/router';
import { DashComponent } from '../dash/dash.component';

@Component({
  selector: 'app-display',
  standalone: true,
  imports: [
    DashServerModule,
    DashComponent,
  ],
  templateUrl: './display.component.html',
  styleUrl: './display.component.sass'
})
export class DisplayComponent implements OnInit {

  // disable contextmenuu
  @HostListener('contextmenu', ['$event'])
  onRightClick(event: any) {
    event.preventDefault();
  }

  displayData?: Display;
  private shareId?: string;

  constructor(
    private dashApi: DashServerDisplay,
    private hubService: DashHubClientService,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.shareId = this.route.snapshot.paramMap.get('displayId') ?? undefined;
    if (this.shareId !== undefined) {
      this.dashApi.get(this.shareId).subscribe(response => {
        this.displayData = response.result;
        this.subscribeToHub();
      });
    }
  }

  // disable left click event
  stopEvent(e: MouseEvent) {
    e.stopPropagation();
  }

  private subscribeToHub(): void {
    this.hubService.displayRefreshed.subscribe(_ => {
      window.location.reload();
    });

    this.hubService.connected.subscribe(isConnected => {
      if (!isConnected) {
        return;
      }
      this.hubService.subsribe(this.displayData?.displayId!);
    });
  }
}
