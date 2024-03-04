import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../../../environment';

@Injectable({
  providedIn: 'root'
})
export class UserAuthService {

  apiUrl: string = environment.authApiUrl;

  constructor(private http: HttpClient) { }

  register(data:any): Observable<any>{
    let payload = {
      name: data.name,
      email: data.email,
      password: data.password,
    }
    return this.http.post(`${this.apiUrl}`, payload)
  }
}
