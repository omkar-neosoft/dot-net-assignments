import { Component } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { AccountsService } from '../../../services/accounts.service';
import { AuthService } from '../../../services/auth.service';
import { AccountType, AlertType, CommanAlert } from '../../../constants';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';

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
    private route: Router
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
        CommanAlert(error.error.message, AlertType.Error);
        console.error(error);
      },
    });
  }

  onSubmit() {
    if (this.accountForm.invalid) return;

    const selectedType = this.accountForm.value.accountType;

    if (this.existingAccounts.includes(selectedType)) {
      CommanAlert(
        `You already have a ${selectedType} account.`,
        AlertType.Error
      );
      return;
    }
    this.createAccountForm.accountType =
      this.accountTypes.indexOf(selectedType);
    this.createAccountForm.balance = this.accountForm.value.initialDeposit;
    this.createAccountForm.userId = this.authService.getUserId() || '';
    this.accountService.createAccount(this.createAccountForm).subscribe({
      next: (response) => {
        CommanAlert('Account created successfully', AlertType.Success);
        this.loadExistingAccounts();
        this.route.navigate(['/accounts']);
      },
      error: (error) => {
        CommanAlert(error.error.message, AlertType.Error);
        console.error(error);
      },
    });
  }
}
