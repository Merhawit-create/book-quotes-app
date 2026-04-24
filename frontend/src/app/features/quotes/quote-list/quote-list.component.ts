import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { QuoteService } from '../../../core/services/quote.service';
import { QuoteModel } from '../../../core/models/quote.model';


@Component({
  selector: 'app-quote-list',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './quote-list.component.html',
  styleUrl: './quote-list.component.css'
})
export class QuoteListComponent implements OnInit {
  quotes: QuoteModel[] = [];
  editingId: number | null = null;
  showForm = false;
  form: FormGroup;

  constructor(
    private fb: FormBuilder,
    private quoteService: QuoteService
  ) {
    this.form = this.fb.group({
      text: ['', Validators.required],
      author: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.loadQuotes();
  }

  loadQuotes(): void {
    this.quoteService.getAll().subscribe(quotes => this.quotes = quotes);
  }

  openAddForm(): void {
    this.showForm = true;
    this.editingId = null;
    this.form.reset();
  }
  onSubmit(): void {
    if (this.form.invalid) return;

    const payload = this.form.getRawValue() as any;

    if (this.editingId) {
      this.quoteService.update(this.editingId, payload).subscribe(() => {
        this.resetForm();
        this.loadQuotes();
      });
    } else {
      this.quoteService.create(payload).subscribe(() => {
        this.resetForm();
        this.loadQuotes();
      });
    }
  }

  editQuote(quote: QuoteModel): void {
    this.showForm = true;
    this.editingId = quote.id;
    this.form.patchValue({
      text: quote.text,
      author: quote.author
    });
  }

  deleteQuote(id: number): void {
    if (!confirm('Are you sure you want to delete this quote?')) return;

    this.quoteService.delete(id).subscribe(() => this.loadQuotes());
  }

  resetForm(): void {
    this.editingId = null;
    this.showForm = false;
    this.form.reset();
  }
  toggleFavorite(quote: QuoteModel): void {
    const updatedQuote = {
      text: quote.text,
      author: quote.author,
      isFavorite: !quote.isFavorite
    };

    this.quoteService.update(quote.id, updatedQuote).subscribe(() => {
      this.loadQuotes();
    });
  }
}
