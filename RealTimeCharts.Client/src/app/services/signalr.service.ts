import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import * as signalR from "@microsoft/signalr"
import { ChartModel } from '../_interfaces/chartmodel.model';

@Injectable({
  providedIn: 'root'
})
export class SignalrService {
  public data: ChartModel[];
  public bradcastedData: ChartModel[];

  constructor(private http: HttpClient) {}

  private hubConnection: signalR.HubConnection
  
  public startConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
                            .withUrl('https://localhost:7095/notification-hub')
                            .withAutomaticReconnect()
                            .configureLogging(signalR.LogLevel.Information)
                            .build();
    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err))

    this.hubConnection.onreconnected(() => {
      this.http.get('https://localhost:7095/notification-hub')
      .subscribe(res => {
        console.log(res);
      })
    })
  }
    
  public addDataListener = () => {
    this.hubConnection.on('ReceiveNotification', (data) => {
      
      console.log(data);
    });
  }

  public broadcastData = () => {
   
    this.hubConnection.invoke('CallFromClient', "Hello from client....")
    .catch(err => console.error(err));
  }

  public addBroadcastChartDataListener = () => {
    this.hubConnection.on('broadcastchartdata', (data) => {
      this.bradcastedData = data;
    })
  }
}
