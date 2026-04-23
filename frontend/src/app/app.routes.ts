import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';
import { LoginComponent } from './features/auth/login/login.component';
import { RegisterComponent } from './features/auth/register/register.component';
import { BookListComponent } from './features/books/book-list/book-list.component';
import { BookFormComponent } from './features/books/book-form/book-form.component';
import { QuoteListComponent } from './features/quotes/quote-list/quote-list.component';

export const routes: Routes = [
  { path: '', redirectTo: 'books', pathMatch: 'full' },

  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },

  { path: 'books', component: BookListComponent, canActivate: [authGuard] },
  { path: 'books/new', component: BookFormComponent, canActivate: [authGuard] },
  { path: 'books/edit/:id', component: BookFormComponent, canActivate: [authGuard] },

  { path: 'quotes', component: QuoteListComponent, canActivate: [authGuard] },

  { path: '**', redirectTo: 'books' }
];
