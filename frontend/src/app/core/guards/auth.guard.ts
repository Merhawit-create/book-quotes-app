import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

/**
 * Protects routes that should only be accessible to authenticated users.
 * If the user is not logged in, they are redirected to the login page.
 */
export const authGuard: CanActivateFn = () => {
  const authService = inject(AuthService);
  const router = inject(Router);
// Allow navigation if the user has a valid token.
  if (authService.isLoggedIn()) {
    return true;
  }
// Redirect unauthenticated users to login.
  router.navigate(['/login']);
  return false;
};
