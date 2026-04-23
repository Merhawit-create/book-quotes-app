import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { BookService } from '../../../core/services/book.service';
import { BookModel } from '../../../core/models/book.model';

@Component({
  selector: 'app-book-list',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './book-list.component.html',
  styleUrl: './book-list.component.css'
})
export class BookListComponent implements OnInit {
  books: BookModel[] = [];

  constructor(
    private bookService: BookService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadBooks();
  }

  loadBooks(): void {
    this.bookService.getAll().subscribe({
      next: books => this.books = books
    });
  }

  deleteBook(id: number): void {
    if (!confirm('Are you sure you want to delete this book?')) return;

    this.bookService.delete(id).subscribe({
      next: () => this.loadBooks()
    });
  }

  editBook(id: number): void {
    this.router.navigate(['/books/edit', id]);
  }
}
