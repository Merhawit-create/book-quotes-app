import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
/**
 * Handles user login using reactive form.
 */
export class LoginComponent {
  errorMessage = '';
  form:FormGroup;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {
    // Initialize form with validation
    this.form = this.fb.group({
      userName: ['', Validators.required],
      password: ['', Validators.required]
    });
  }
  /**
   * Submits login request to backend.
   */
  onSubmit(): void {
    if (this.form.invalid) return;

    this.authService.login(this.form.getRawValue() as any).subscribe({
      next: () => this.router.navigate(['/books']),
      error: () => this.errorMessage = 'Invalid username or password.'
    });
  }
}
