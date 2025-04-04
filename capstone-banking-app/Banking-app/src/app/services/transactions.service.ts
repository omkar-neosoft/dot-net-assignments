import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { APP_CONSTANTS } from '../constants';

@Injectable({
  providedIn: 'root',
})
export class TransactionsService {
  constructor(private http: HttpClient) {}

  getLoggedInUserAccountId(userId: string): Observable<any> {
    return this.http.get(
      `${APP_CONSTANTS.apiUrls.getLoggedInUserAccountId}/${userId}`,
      {
        withCredentials: true,
      }
    );
  }

  createTransaction(data: any): Observable<any> {
    return this.http.post(`${APP_CONSTANTS.apiUrls.createTransaction}`, data, {
      withCredentials: true,
    });
  }

  getUserTransactions(userId: string): Observable<any> {
    // return this.http.get(
    //   `${APP_CONSTANTS.apiUrls.getUserTransactions}/${accountId}`,
    //   {
    //     withCredentials: true,
    //   }
    // );
    return this.http.get(
      `${APP_CONSTANTS.apiUrls.getUserTransactions}/${userId}`,
      {
        withCredentials: true,
      }
    );
  }
}
