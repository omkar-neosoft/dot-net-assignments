import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { MatIcon } from '@angular/material/icon';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [CommonModule, MatIcon],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.css',
})
export class SidebarComponent {
  showSidebar = true;

  constructor(private router: Router) {
    this.checkSidebarVisibility();
  }

  checkSidebarVisibility() {
    const hiddenRoutes = ['/auth', '/register']; // Hide sidebar for login & register
    this.showSidebar = !hiddenRoutes.includes(this.router.url);
  }

  toggleSidebar() {
    const sidebar = document.querySelector('.sidebar') as HTMLElement;
    sidebar.classList.toggle('active');
  }
}
