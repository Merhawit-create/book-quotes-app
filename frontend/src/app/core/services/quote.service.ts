import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { QuoteModel } from '../models/quote.model.';

@Injectable({
  providedIn: 'root'
})
export class QuoteService {
  constructor(private http: HttpClient) {}

  getAll(): Observable<QuoteModel[]> {
    return this.http.get<QuoteModel[]>(`${environment.apiUrl}/quotes`);
  }

  getById(id: number): Observable<QuoteModel> {
    return this.http.get<QuoteModel>(`${environment.apiUrl}/quotes/${id}`);
  }

  create(quote: Omit<QuoteModel, 'id'>): Observable<QuoteModel> {
    return this.http.post<QuoteModel>(`${environment.apiUrl}/quotes`, quote);
  }

  update(id: number, quote: Omit<QuoteModel, 'id'>): Observable<QuoteModel> {
    return this.http.put<QuoteModel>(`${environment.apiUrl}/quotes/${id}`, quote);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${environment.apiUrl}/quotes/${id}`);
  }
}
