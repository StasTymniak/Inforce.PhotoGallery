import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { AuthApiService } from '../../api/services/auth-api.service';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
})
export class LayoutComponent {

  constructor(public authService: AuthService, private authApiService: AuthApiService) { }

  logout(): void {
    this.authApiService.logout()
      .subscribe({
        next: () => {
          this.authService.logout();
        }
      })
  }
}
