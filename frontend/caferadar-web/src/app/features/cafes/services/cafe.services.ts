import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { API_URL } from '../../../config/api.config';
import { CafeStatus } from '../models/cafe-status';

@Injectable({
  providedIn: 'root'
})
export class CafeService {
  private http = inject(HttpClient);
  private apiUrl = inject(API_URL); 
  private espIp = "http://YOUR_DEVICE_IP";

  getStatus(): Observable<CafeStatus[]> {
    return this.http.get<CafeStatus[]>(`${this.apiUrl}/cafes/status`);
  }

  showOnEsp() {
   return this.http.get(`${this.espIp}/show`, { responseType: 'text' });
}
}

