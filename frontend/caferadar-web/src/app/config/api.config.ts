import { InjectionToken } from '@angular/core';
import { provideHttpClient, withFetch } from '@angular/common/http';


export const API_URL = new InjectionToken<string>('API_URL', {
  providedIn: 'root',
  factory: () => 'http://YOUR_BACKEND_URL'
});
