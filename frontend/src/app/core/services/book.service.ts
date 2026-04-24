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
  /**
   * Gets all books that belong to the authenticated user.
   */
  getAll(): Observable<BookModel[]> {
    return this.http.get<BookModel[]>(`${environment.apiUrl}/books`);
  }
  /**
   * Gets a single book by ID.
   */
  getById(id: number): Observable<BookModel> {
    return this.http.get<BookModel>(`${environment.apiUrl}/books/${id}`);
  }
  /**
   * Creates a new book.
   */
  create(book: Omit<BookModel, 'id'>): Observable<BookModel> {
    return this.http.post<BookModel>(`${environment.apiUrl}/books`, book);
  }
  /**
   * Updates an existing book.
   */
  update(id: number, book: Omit<BookModel, 'id'>): Observable<BookModel> {
    return this.http.put<BookModel>(`${environment.apiUrl}/books/${id}`, book);
  }

  /**
   * Deletes a book by ID.
   */
  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${environment.apiUrl}/books/${id}`);
  }
}
