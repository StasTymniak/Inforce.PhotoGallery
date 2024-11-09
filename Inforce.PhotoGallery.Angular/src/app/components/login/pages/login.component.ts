import { Component } from '@angular/core';
import { AuthService } from '../../../services/auth.service';
import { Router } from '@angular/router';
import { AuthApiService } from '../../../api/services/auth-api.service';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
})
export class LoginComponent {

  username = '';
  password = '';

  constructor(private authService: AuthService,
    private authApiService: AuthApiService,
    private router: Router) {
    console.log('Constructor called');
  }

  ngOnInit(): void {
    if (this.authService.isLoggedIn()) {
      this.router.navigate(['/']);
    }
  }

  onLogin() {
    console.log(this.username, this.password)
    this.authApiService.login(this.username, this.password).subscribe(
      (response) => {
        console.log('Login successful:', response);
      },
      (error) => {
        console.error('Login failed:', error);
      }
    );
  }

  goGuest() {
    this.router.navigate(['/']);
  }
}
