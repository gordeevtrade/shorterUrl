import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { FullUrl } from 'src/app/models/FullUrl';
import { ShortUrl } from 'src/app/models/ShortUrl';
import {environment} from "../../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class UrlShortService {

  private  _registrationUserUrl =  `${environment.apiUrl}/Url`;

  constructor(private http: HttpClient ) {

  }

 public getUrls(): Observable<ShortUrl[]> {
  const url = `${this._registrationUserUrl}/urls`;
  return this.http.get<ShortUrl[]>(url);
  }



  public redirectToUrl(shortCode: string): Observable<FullUrl> {
    const url = `${this._registrationUserUrl}/shortCode?shortCode=${encodeURIComponent(shortCode)}`;
    return this.http.post<FullUrl>(url,{});
  }
  
  public deleteUrl(id: number): Observable<any> {
    const url = `${this._registrationUserUrl}/delete/${id}`;
    return this.http.delete(url);
  }
  
  public createUrl(fullUrl: FullUrl): Observable<any> {
    const url = `${this._registrationUserUrl}/create`;
    return this.http.post<any>(url, fullUrl);
  }
  

}
