import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import * as signalR from "@microsoft/signalr"
import { MessageModel } from '../_interfaces/chartmodel.model';

@Injectable({
  providedIn: 'root'
})
export class SignalrService {
  public activities: string[] = []; 
public signalRConnectionId = '';

  constructor(private http: HttpClient) {}

  private hubConnection: signalR.HubConnection
  
  public startConnection = () => {
 
    this.addNewActivity("Start connecting to the server...");

    this.hubConnection = new signalR.HubConnectionBuilder()
                            .withUrl('https://localhost:7095/notification-hub')
                            .withAutomaticReconnect()
                            .configureLogging(signalR.LogLevel.Information)
                            .build();
    this.hubConnection
      .start()
      .then(() => this.addNewActivity("Connection started..."))
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
      
      this.addNewActivity("API notification recived. " + data);
    });
  }

  public ConnectedNotificationMetod = () => {
    this.hubConnection.on('ConnectedNotification', (data) => {
       
       this.signalRConnectionId = data;

       this.addNewActivity("Connection id -> "+this.signalRConnectionId);
    });
  }

  public broadcastData = () => {
   
    this.hubConnection.invoke('CallFromClient', "Hello from client....")
    .then(c=> this.addNewActivity("After successfully calling server method..."))
    .catch(err => console.error(err));
  }

  public callFromClient = () => {
    this.hubConnection.on('callFromClient', (data) => {
     alert("calling by server...");
    })
  }
  public addNewActivity(actitity){
    this.activities.push(actitity);
  }

  public clearAcitvities(){
    this.activities = [];
  }
}
