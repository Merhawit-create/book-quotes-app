import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { QuoteModel } from '../models/quote.model';

@Injectable({
  providedIn: 'root'
})
export class QuoteService {
  constructor(private http: HttpClient) {}
  /**
   * Gets all quotes that belong to the authenticated user.
   */
  getAll(): Observable<QuoteModel[]> {
    return this.http.get<QuoteModel[]>(`${environment.apiUrl}/quotes`);
  }
  /**
   * Gets a single quote by ID.
   */
  getById(id: number): Observable<QuoteModel> {
    return this.http.get<QuoteModel>(`${environment.apiUrl}/quotes/${id}`);
  }
  /**
   * Creates a new quote.
   */
  create(quote: Omit<QuoteModel, 'id'>): Observable<QuoteModel> {
    return this.http.post<QuoteModel>(`${environment.apiUrl}/quotes`, quote);
  }
  /**
   * Updates an existing quote, including favorite status.
   */
  update(id: number, quote: Omit<QuoteModel, 'id'>): Observable<QuoteModel> {
    return this.http.put<QuoteModel>(`${environment.apiUrl}/quotes/${id}`, quote);
  }
  /**
   * Deletes a quote by ID.
   */
  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${environment.apiUrl}/quotes/${id}`);
  }
}
