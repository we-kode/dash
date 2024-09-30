import { EventEmitter, Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';

@Injectable({
  providedIn: 'root'
})
export class DashHubClientService {

  private baseUrl = 'http://172.23.4.74:5051';
  private hubConnection: signalR.HubConnection;
  displayRefreshed: EventEmitter<void> = new EventEmitter();
  connected: EventEmitter<boolean> = new EventEmitter();

  constructor() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(this.baseUrl + '/hub/display')
      .withAutomaticReconnect()
      .build();
  
    this.hubConnection
      .start()
      .then(() => this.connected.emit(true))
      .catch(err => console.error('Error connecting to Display hub:', err));
  
    this.hubConnection.on('DASH_DISPLAY_REFRESH', () => {
      this.displayRefreshed.emit();
    });
  }

  async subsribe(displayId: string) {
    await this.hubConnection.invoke('Subscribe', displayId);
  }
}
