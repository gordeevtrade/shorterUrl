import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class JwtService {

  constructor() { }

  public getToken(): string {
    const token = localStorage.getItem('token');
    return token ? token : '';
  }


  public saveToken(token: string) {
 
    localStorage.setItem('token', token);
  }

  
  public isTokenExpired(): boolean {
    const expiresIn = localStorage.getItem('expiresIn');
    if (expiresIn) {
      const expiresDate = new Date(expiresIn);
      const currentDateTime = new Date();
      return currentDateTime < expiresDate;
    }
    return true;
  }
  
  
 public removeToken() {
    localStorage.removeItem('token');
    localStorage.removeItem('expiresIn');
  }
}
