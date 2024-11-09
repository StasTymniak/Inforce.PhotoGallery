import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(private router: Router) { }

  logout(): void {
    localStorage.removeItem('access_token');
    this.router.navigate(['/login']);
  }

  decodedToken() {
    const jwtHelper = new JwtHelperService();
    const token = localStorage.getItem("access_token")!;
    return jwtHelper.decodeToken(token)
  }

  getRoleFromToken(): string {
    const token = this.decodedToken();

    if (token === null)
      return '';

    return token.role;
  }

  isAdmin(): boolean {
    const token = this.decodedToken();

    if (token === null)
      return false;

    return token.role === 'Admin';
  }

  getUsernameFromToken(): string {
    const token = this.decodedToken();

    if (token === null)
      return '';

    return token.unique_name;
  }

  getUserIdFromToken(): number {
    const token = this.decodedToken();

    if (token === null)
      return 0;

    return +token.nameid;
  }

  isLoggedIn(): boolean {
    if (localStorage.getItem("access_token") === null) {
      return false
    }
    else {
      return true
    }
  }
}
