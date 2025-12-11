import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class EspService {
  private espUrl = "http://YOUR_DEVICE_IP"; 

  constructor(private http: HttpClient) {}

  showOnDisplay() {
    return this.http.get(`${this.espUrl}/show`, { responseType: 'text' });
  }
}
