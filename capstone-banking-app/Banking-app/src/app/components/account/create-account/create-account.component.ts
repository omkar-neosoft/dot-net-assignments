import { Component } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { AccountsService } from '../../../services/accounts.service';
import { AuthService } from '../../../services/auth.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AccountType } from '../../../constants';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-create-account',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './create-account.component.html',
  styleUrl: './create-account.component.css',
})
export class CreateAccountComponent {
  accountForm: FormGroup;
  existingAccounts: string[] = [];
  accountTypes = AccountType;
  createAccountForm: {
    userId?: string;
    balance?: number;
    accountType?: number;
  } = {};

  constructor(
    private fb: FormBuilder,
    private accountService: AccountsService,
    private authService: AuthService,
    private snackBar: MatSnackBar
  ) {
    this.accountForm = this.fb.group({
      accountType: ['', Validators.required],
      initialDeposit: ['', [Validators.required, Validators.min(500)]],
    });
  }

  ngOnInit() {
    this.loadExistingAccounts();
  }

  loadExistingAccounts() {
    const userId = this.authService.getUserId() || '';
    this.accountService.getAccountDetails(userId).subscribe({
      next: (accounts) => {
        accounts.map((account: { accountType: number }) => {
          this.existingAccounts.push(this.accountTypes[account.accountType]);
        });
      },
      error: (error) => {
        alert(error);
      },
    });
  }

  onSubmit() {
    if (this.accountForm.invalid) return;

    const selectedType = this.accountForm.value.accountType;

    // Restrict duplicate account type
    if (this.existingAccounts.includes(selectedType)) {
      this.snackBar.open(
        `You already have a ${selectedType} account.`,
        'Close',
        { duration: 3000 }
      );
      return;
    }
    this.createAccountForm.accountType =
      this.accountTypes.indexOf(selectedType);
    this.createAccountForm.balance = this.accountForm.value.initialDeposit;
    this.createAccountForm.userId = this.authService.getUserId() || '';
    this.accountService.createAccount(this.createAccountForm).subscribe({
      next: (response) => {
        this.snackBar.open('Account created successfully!', 'Close', {
          duration: 3000,
        });
        this.loadExistingAccounts(); // Refresh user account list
      },
      error: (error) => {
        this.snackBar.open('Failed to create account. Try again.', 'Close', {
          duration: 3000,
        });
      },
    });
  }
}
