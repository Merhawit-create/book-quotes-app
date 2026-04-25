import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
/**
 * Handles user registration.
 */
export class RegisterComponent {
  errorMessages: string[] = [];
  form: FormGroup;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {
    // Form validation
    this.form = this.fb.group({
      userName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }

  /**
   * Sends register request.
   */
  onSubmit(): void {
    if (this.form.invalid) {
      this.errorMessages = ['Please fill in all fields correctly.'];
      return;
    }

    this.authService.register(this.form.getRawValue() as any).subscribe({
      next: () => this.router.navigate(['/books']),
      error: err => {
        this.errorMessages = err.error?.errors || [
          'Registration failed. Please check your username, email, and password.'
        ];
      }
    });
  }
}
