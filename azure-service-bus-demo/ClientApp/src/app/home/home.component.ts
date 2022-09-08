import { Component, Inject, OnInit } from '@angular/core';

import * as signalR from "@microsoft/signalr"
import { environment } from 'src/environments/environment';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  public messages: MessageModel[] = [];
  public isSignalConnected: boolean = false;

  private baseUrl: string = environment.serverUrl;

  ngOnInit(): void {
    const hubConnection = new signalR.HubConnectionBuilder()
      .configureLogging(signalR.LogLevel.Information)
      .withUrl(this.baseUrl + 'chatHub')
      .build();

      hubConnection.on("OnMessageRecived", (message: MessageModel) => {
        this.messages.push(message);
      });

      hubConnection.start()
        .then(() => { this.isSignalConnected = true })
        .catch(() => { this.isSignalConnected = true } );
  }
}

interface MessageModel {
  username: string;
  message: string;
}