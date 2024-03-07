import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, catchError, map, of, throwError } from 'rxjs';
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
  
  loadCurrentUser(token: string | null) {
    if (token == null) {
      this.currentUserSource.next(null);
      return of(null);
    }

    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http.get<User>(`https://localhost:7000/api/current-user`, {headers}).pipe(
      map(user => {
        if (user) {
          localStorage.setItem('token', token);
          this.currentUserSource.next(user);
          return user;
        } else {
          return null;
        }
      })
    )
  }

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
    .pipe(
      map(user => {
        localStorage.setItem('token', user.token);
        this.currentUserSource.next(user);
        return user; // Return the user data for further processing if needed
      }),
      catchError(error => {
        // Handle the error here (e.g., log it, display a message to the user)
        console.error('Error during login:', error);
        
        // You can rethrow the error to propagate it to the subscriber if needed
        return throwError(error);
      })
    );
  }

  logout() {
    localStorage.removeItem('token');
    this.currentUserSource.next(null);
    this.router.navigateByUrl('/');
  }
}
