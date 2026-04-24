import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BookService } from '../../../core/services/book.service';

@Component({
  selector: 'app-book-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './book-form.component.html',
  styleUrl: './book-form.component.css'
})
export class BookFormComponent implements OnInit {
  isEditMode = false;
  bookId = 0;
  form: FormGroup;


  constructor(
    private fb: FormBuilder,
    private bookService: BookService,
    private route: ActivatedRoute,
    private router: Router
  ) {this.form = this.fb.group({
    title: ['', Validators.required],
    author: ['', Validators.required],
    publishedDate: ['', Validators.required]
  });
  }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');

    if (id) {
      this.isEditMode = true;
      this.bookId = +id;

      this.bookService.getById(this.bookId).subscribe(book => {
        this.form.patchValue({
          title: book.title,
          author: book.author,
          publishedDate: book.publishedDate.split('T')[0]
        });

      });
    }
  }
  onSubmit(): void {
    if (this.form.invalid) return;

    const payload = this.form.getRawValue() as any;

    if (this.isEditMode) {
      this.bookService.update(this.bookId, payload).subscribe(() => {
        this.router.navigate(['/books']);
      });
    } else {
      this.bookService.create(payload).subscribe(() => {
        this.router.navigate(['/books']);
      }
      );
    }
  }
}
