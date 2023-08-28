import { Component, OnInit } from '@angular/core';
import { SignalrService } from './services/signalr.service';
import { HttpClient } from '@angular/common/http';
import { ChartConfiguration, ChartType } from 'chart.js';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {


  constructor(public signalRService: SignalrService, private http: HttpClient) { }

  ngOnInit() {
    this.signalRService.startConnection();
    this.signalRService.addDataListener();
    this.signalRService.ConnectedNotificationMetod();
    this.signalRService.callFromClient();

  }

  private startHttpRequest = () => {
    this.signalRService.addNewActivity("Calling API...")
    this.http.get('https://localhost:7095/WeatherForecast')
      .subscribe(res => {
        console.log(res);
        this.signalRService.addNewActivity("Successfully getting response from API.")

      })
  }

  public bntClicked = (event) => {
    this.signalRService.addNewActivity("Calling server method...");
    this.signalRService.broadcastData();
  }

  public callApi = (event) => {
    console.log(event);
    this.startHttpRequest();
  }
}
