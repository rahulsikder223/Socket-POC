import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { WebsocketService } from './websocket.service';
import { environment } from 'src/environments/environment';

@Injectable()
export class SocketService {
    public messages: Subject<string>;

    constructor(private ws: WebsocketService) {
        this.messages = <Subject<string>> ws
        .connect(environment.ws_url)
        .map((response: MessageEvent): string => {
            return response.data;
        })
    }
}