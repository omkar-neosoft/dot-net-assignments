import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CookieService } from 'ngx-cookie-service';
import { BehaviorSubject, Observable } from 'rxjs';
import { APP_CONSTANTS } from '../constants';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private tokenKey = 'AuthToken';
  private authTokenSubject = new BehaviorSubject<string | null>(null); // Global state
  authToken$ = this.authTokenSubject.asObservable(); // Expose as Observable

  constructor(private http: HttpClient, private cookieService: CookieService) {
    this.loadTokenFromCookies(); // Load token on service init
  }

  isLoggedIn(): boolean {
    return this.cookieService.check(this.tokenKey); // Check if token exists in cookies
  }

  login(credentials: { email: string; password: string }): Observable<any> {
    return this.http.post(APP_CONSTANTS.apiUrls.loginUrl, credentials);
  }

  register(credentials: {
    fullName: string;
    email: string;
    password: string;
    confirmPassword: string;
    username: string;
  }): Observable<any> {
    return this.http.post(APP_CONSTANTS.apiUrls.regiterUrl, credentials);
  }

  storeToken(token: string): void {
    this.authTokenSubject.next(token); // Update global state
    this.cookieService.set(
      'AuthToken',
      token
      // {
      // secure: true,
      // sameSite: 'Strict',
      // }
    );
  }

  storeUserData(response: any) {
    console.log({ responseFromUser: response });
    this.storeToken(response.token);
    this.setUserCookies(response);
  }

  setUserCookies(userData: any) {
    console.log({ userData });
    this.cookieService.set(
      'userid',
      userData.id
      //   {
      //   secure: true,
      //   sameSite: 'Strict',
      // }
    );
  }

  getToken(): string | null {
    this.authTokenSubject.next(this.cookieService.get(this.tokenKey));
    return this.authTokenSubject.value;
  }

  getUserId(): string | null {
    return this.cookieService.get('userid');
  }

  logout(): void {
    this.authTokenSubject.next(null);
    this.cookieService.delete('AuthToken');
  }

  isAuthenticated(): boolean {
    return !!this.getToken();
  }

  // Load token from cookies (on page refresh)
  private loadTokenFromCookies(): void {
    const savedToken = this.cookieService.get(this.tokenKey);
    if (savedToken) {
      this.authTokenSubject.next(savedToken);
    }
  }
}
