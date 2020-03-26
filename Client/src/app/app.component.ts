import { Component } from '@angular/core';
import { SocketService } from './socket.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'socket-client';
  messageList = [];

  constructor(private cs: SocketService) {
    cs.messages.subscribe(response => {
      if (response) {
        this.messageList.push(response);
        console.log("Received from Server: " + response);
      }
    });
  }

  sendMessage() {
    this.cs.messages.next("Message from client...");
  }
}
