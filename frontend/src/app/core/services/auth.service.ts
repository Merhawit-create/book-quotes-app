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
  // Stores the current logged-in user state for the UI.
  currentUser = signal<string | null>(localStorage.getItem(this.tokenKey) ? 'Logged User' : null);

  constructor(
    private http: HttpClient,
    private router: Router
  ) {}
  /**
   * Registers a new user and stores the JWT token when registration succeeds.
   */
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
  /**
   * Logs in an existing user and stores the JWT token when login succeeds.
   */
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

  /**
   * Returns the JWT token from local storage.
   */
  getToken(): string | null {
    return localStorage.getItem('token');
  }

  /**
   * Checks if the user is logged in and if the token is still valid.
   */
  isLoggedIn(): boolean {
    const token = this.getToken();

    if (!token) return false;

    if (this.isTokenExpired(token)) {
      this.logout();
      return false;
    }

    return true;
  }
  /**
   * Checks whether the JWT token has expired.
   */
  private isTokenExpired(token: string): boolean {
    try {
      const payload = JSON.parse(atob(token.split('.')[1]));
      return Date.now() >= payload.exp * 1000;
    } catch {
      return true;
    }
  }
  /**
   * Logs out the user by removing the token and redirecting to login.
   */
  logout(): void {
    localStorage.removeItem('token');
    this.currentUser.set(null);
    this.router.navigate(['/login']);
  }



}
