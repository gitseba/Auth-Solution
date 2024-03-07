import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, map } from 'rxjs';
import { environment } from '../../../../../environment';
import { User } from 'src/app/models/user';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class UserAuthService {

  registrationApiUrl: string = environment.registrationApiUrl;
  loginApiUrl: string = environment.loginApiUrl;

  private currentUserSource: BehaviorSubject<User | null> = new BehaviorSubject<User | null>(null);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient, private router: Router) { }

  register(data: any): Observable<any> {
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

  login(data: any): Observable<any> {
    let payload = {
      email: data.email,
      password: data.password,
    }
    /* ensure that you set the withCredentials option to true when making the HTTP request to include cookies in the request. 
    Angular's HttpClient will automatically handle the cookies sent by the server and store them.  */
    return this.http.post<any>(`${this.loginApiUrl}`, payload)
      .pipe(map(user => {
        localStorage.setItem('token', user.token);
        this.currentUserSource.next(user);
      }))
  }

  logout() {
    localStorage.removeItem('token');
    this.currentUserSource.next(null);
    this.router.navigateByUrl('/');
  }
}
