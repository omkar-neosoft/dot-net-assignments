import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavbarComponent } from './layout/navbar/navbar.component';
import { SidebarComponent } from './layout/sidebar/sidebar.component';
import { HeaderComponent } from './layout/header/header.component';
import { AuthService } from './services/auth.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, HeaderComponent, CommonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent {
  authToken: string | null = null;

  constructor(private authService: AuthService) {}

  ngOnInit() {
    this.authService.authToken$.subscribe((token) => {
      this.authToken = token;
    });
    this.authToken = this.authService.getToken();
  }

  title = 'banking-app';
}
