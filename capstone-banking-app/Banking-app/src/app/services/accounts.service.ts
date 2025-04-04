import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { APP_CONSTANTS } from '../constants';

@Injectable({
  providedIn: 'root',
})
export class AccountsService {
  constructor(private http: HttpClient) {}

  getAccountDetails(userId: string): Observable<any> {
    return this.http.get(
      `${APP_CONSTANTS.apiUrls.getLoggedInUserAccountId}/${userId}`,
      {
        withCredentials: true,
      }
    );
    // return this.http.get(`${this.apiUrl}/loggedInUserAccount`);
  }

  createAccount(accountData: any): Observable<any> {
    return this.http.post(
      `${APP_CONSTANTS.apiUrls.createAccount}`,
      accountData
    );
  }
}
