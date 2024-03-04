import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserAuthService {

  constructor(private http: HttpClient) { }

  register(data:any): Observable<any>{
    let payload = {
      name: data.name,
      email: data.email,
      password: data.password,
      password_confirmation: data.confirmPassword
    }
    return this.http.post('https://localhost:7235/api/account/registeree', payload)
  }
}
