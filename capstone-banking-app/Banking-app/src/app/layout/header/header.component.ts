import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { MatIcon } from '@angular/material/icon';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [MatIcon, CommonModule, RouterModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css',
})
export class HeaderComponent {
  mobileMenuOpen = false;
  isLoogedIn = false;
  authToken: string | null = null;

  constructor(private router: Router, private authService: AuthService) {
    // this.authService.authToken$.subscribe((token) => {
    //   this.authToken = token;
    // });
  }

  ngOnInit() {
    // if (this.authService.getToken()) {
    //   this.isLoogedIn = true;
    // }

    // ✅ Subscribe in ngOnInit() to ensure authToken is set correctly
    this.authService.authToken$.subscribe((token) => {
      this.authToken = token;
    });

    // ✅ Immediately check if token is available after refresh
    this.authToken = this.authService.getToken();
  }

  toggleMobileMenu() {
    this.mobileMenuOpen = !this.mobileMenuOpen;
  }

  logout() {
    this.authService.logout();
    this.router.navigate(['/auth']);
  }
}
