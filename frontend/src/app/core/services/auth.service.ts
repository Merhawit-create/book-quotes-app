import { Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable, tap } from 'rxjs';
import { environment } from '../../../environments/environment';
import { LoginModel } from '../models/login.model';
import { RegisterModel } from '../models/register.model';
import { AuthResponseModel } from '../models/auth-response.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly tokenKey = 'token';
  currentUser = signal<string | null>(localStorage.getItem(this.tokenKey) ? 'Logged User' : null);

  constructor(
    private http: HttpClient,
    private router: Router
  ) {}

  register(data: RegisterModel): Observable<AuthResponseModel> {
    return this.http.post<AuthResponseModel>(`${environment.apiUrl}/auth/register`, data).pipe(
      tap(response => {
        if (response.isSuccess) {
          localStorage.setItem(this.tokenKey, response.token);
          this.currentUser.set(response.userName);
        }
      })
    );
  }

  login(data: LoginModel): Observable<AuthResponseModel> {
    return this.http.post<AuthResponseModel>(`${environment.apiUrl}/auth/login`, data).pipe(
      tap(response => {
        if (response.isSuccess) {
          localStorage.setItem(this.tokenKey, response.token);
          this.currentUser.set(response.userName);
        }
      })
    );
  }


  getToken(): string | null {
    return localStorage.getItem('token');
  }


  isLoggedIn(): boolean {
    const token = this.getToken();

    if (!token) return false;

    if (this.isTokenExpired(token)) {
      this.logout();
      return false;
    }

    return true;
  }

  private isTokenExpired(token: string): boolean {
    try {
      const payload = JSON.parse(atob(token.split('.')[1]));
      return Date.now() >= payload.exp * 1000;
    } catch {
      return true;
    }
  }

  logout(): void {
    localStorage.removeItem('token');
    this.currentUser.set(null);
    this.router.navigate(['/login']);
  }



}
