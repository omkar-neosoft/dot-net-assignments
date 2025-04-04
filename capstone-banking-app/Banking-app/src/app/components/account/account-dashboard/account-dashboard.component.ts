import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { AccountsService } from '../../../services/accounts.service';
import { AuthService } from '../../../services/auth.service';
import { AccountType } from '../../../constants';
import { RouterModule } from '@angular/router';

interface Account {
  id: string;
  balance: number;
  accountType: number;
}

@Component({
  selector: 'app-account-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    CommonModule,
    RouterModule,
  ],
  templateUrl: './account-dashboard.component.html',
  styleUrl: './account-dashboard.component.css',
})
export class AccountDashboardComponent implements OnInit {
  accounts!: Account[];
  private accountsService = inject(AccountsService);
  private authService = inject(AuthService);
  userId: string = '';
  accountType = AccountType;

  ngOnInit() {
    this.userId = this.authService.getUserId() || '';
    this.loadAccountDetails();
  }

  loadAccountDetails() {
    this.accountsService.getAccountDetails(this.userId).subscribe({
      next: (data) => {
        this.accounts = data;
      },
    });
  }
}
