import { Component } from '@angular/core';

@Component({
  selector: 'app-profile',
  standalone: true,
  templateUrl: './profile.html',
  styleUrl: './profile.css'
})
/**
 * Extracts user data from JWT token.
 */
export class Profile {

  getUserData() {
    const token = localStorage.getItem('token');
    if (!token) return null;

    const payload = JSON.parse(atob(token.split('.')[1]));
    console.log(payload);

    return {
      username: payload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name']
        || payload['unique_name']
        || payload['name'],

      email: payload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress']
        || payload['email']
    };
  }
}
