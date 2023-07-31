import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { JwtService } from './jwt.service';
import {environment} from "../../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class AuthApiService {

  private  _registrationUserUrl =  `${environment.apiUrl}/User`;

  constructor(private http: HttpClient,private jwt: JwtService ) {

   }
 public login(username: string, password: string): Observable<any> {
    return this.http.post(`${this._registrationUserUrl+'/login'}`, { username, password }, {responseType: 'text'}).pipe(
      tap(response => {
        this.jwt.saveToken(response);
      })

    );
  }

 public register(username: string, password: string): Observable<any> {
    return this.http.post(`${this._registrationUserUrl}`+'/register',  { username, password }, {responseType: 'text'});
  }

  public logout() {
    this.jwt.removeToken();
  }

}
