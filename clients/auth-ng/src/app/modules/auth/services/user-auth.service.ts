import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../../../environment';

@Injectable({
  providedIn: 'root'
})
export class UserAuthService {

  registrationApiUrl: string = environment.registrationApiUrl;
  loginApiUrl: string = environment.loginApiUrl;

  constructor(private http: HttpClient) { }

  register(data:any): Observable<any>{
    let payload = {
      name: data.name,
      email: data.email,
      password: data.password,
    }
    return this.http.post<any>(`${this.registrationApiUrl}`, payload)
  }

  checkEmailExists(email: string) {
    return this.http.get<boolean>(`${this.registrationApiUrl}` + '/checkEmail?email=' + email);
  }

  login(data:any): Observable<any>{
    let payload = {
      email: data.email,
      password: data.password,
    }
    /* ensure that you set the withCredentials option to true when making the HTTP request to include cookies in the request. 
    Angular's HttpClient will automatically handle the cookies sent by the server and store them.  */
    return this.http.post<any>(`${this.loginApiUrl}`, payload)
  }
}
