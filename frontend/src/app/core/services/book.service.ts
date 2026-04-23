import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { BookModel } from '../models/book.model';

@Injectable({
  providedIn: 'root'
})
export class BookService {
  constructor(private http: HttpClient) {}

  getAll(): Observable<BookModel[]> {
    return this.http.get<BookModel[]>(`${environment.apiUrl}/books`);
  }

  getById(id: number): Observable<BookModel> {
    return this.http.get<BookModel>(`${environment.apiUrl}/books/${id}`);
  }

  create(book: Omit<BookModel, 'id'>): Observable<BookModel> {
    return this.http.post<BookModel>(`${environment.apiUrl}/books`, book);
  }

  update(id: number, book: Omit<BookModel, 'id'>): Observable<BookModel> {
    return this.http.put<BookModel>(`${environment.apiUrl}/books/${id}`, book);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${environment.apiUrl}/books/${id}`);
  }
}
