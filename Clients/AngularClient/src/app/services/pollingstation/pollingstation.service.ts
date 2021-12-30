import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import axios from 'axios';

@Injectable({
  providedIn: 'root'
})
export class PollingstationService {

  apiUrl: string = 'http://localhost:3000';
  headers = new HttpHeaders().set('Content-Type', 'application/json');

  constructor(private http: HttpClient) { }

  sendVote(data: any): Observable<any> {
    let API_URL = `${this.apiUrl}/pollingstation/recevieVote`;
    return this.http.post(API_URL, data)
      .pipe(
        catchError(this.error)
      )
  }
  async getServerStatus() {
    let API_URL = `${this.apiUrl}/pollingstation/getServerStatus`;
    try {
      var res = await axios.get(API_URL);
      return res.data.Status;
    } catch (error) {
      return false
    }
  }

  async startElection() {
    let API_URL = `${this.apiUrl}/pollingstation/startElection`;
    await axios.post(API_URL, {
      "state": 1,
      "period": 30000
    });
  }

  async stopElection() {
    let API_URL = `${this.apiUrl}/pollingstation/startElection`;
    await axios.post(API_URL, {
      "state": 0,
      "period": 30000
    });
  }
  // Handle Errors
  error(error: HttpErrorResponse) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      errorMessage = error.error.message;
    } else {
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    console.log(errorMessage);
    return throwError(errorMessage);
  }
}
