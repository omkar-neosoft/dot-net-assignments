import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { TransactionsService } from '../../../services/transactions.service';
import { AuthService } from '../../../services/auth.service';
import { Router } from '@angular/router';
import { AlertType, CommanAlert } from '../../../constants';

@Component({
  selector: 'app-create-transaction',
  standalone: true,
  imports: [ReactiveFormsModule, FormsModule, CommonModule],
  templateUrl: './create-transaction.component.html',
  styleUrl: './create-transaction.component.css',
})
export class CreateTransactionComponent implements OnInit {
  transactionForm!: FormGroup;

  transactionTypes = [
    { value: 0, label: 'Deposit' },
    { value: 1, label: 'Withdrawal' },
    { value: 2, label: 'Transfer' },
  ];

  userId: string = '';
  accountId: string = '';

  isTransactionTypeTransfer = false;

  constructor(
    private fb: FormBuilder,
    private transactionsService: TransactionsService,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.transactionForm = this.fb.group({
      accountId: [], // Sender Account ID
      transactionType: ['', Validators.required],
      amount: [null, [Validators.required, Validators.min(1)]],
      description: [''],
      toAccountId: [''], // Receiver Account ID (for transfers)
    });

    this.userId = this.authService.getUserId() || '';
    this.transactionsService.getLoggedInUserAccountId(this.userId).subscribe({
      next: (response) => {
        // console.log({ response });
        // this.accountId = response[0].id;
        this.accountId = response.map((data: { id: string }) => data.id);

        console.log({ accountId: this.accountId });
      },
      error: (error) => {
        CommanAlert(error.error.message, AlertType.Error);
        console.error(error);
      },
    });
  }

  isTransactionTypeChange(event: any) {
    console.log({ event });
    // console.log({eventtarget: event});
    if (event.target.value === '2') {
      this.isTransactionTypeTransfer = true;
    } else {
      this.isTransactionTypeTransfer = false;
    }
  }

  // Function to check if transfer is selected
  isTransfer(): boolean {
    return this.transactionForm.get('transactionType')?.value === 'Transfer';
  }

  // Submit form data
  onSubmit(): void {
    console.log('onsubmit');
    if (this.transactionForm.valid) {
      const transactionData = this.transactionForm.value;

      if (typeof transactionData.transactionType === 'string') {
        transactionData.transactionType = parseInt(
          transactionData.transactionType
        );
      }

      const apiData = {
        accountId: transactionData.accountId,
        fromAccountId:
          transactionData.toAccountId === ''
            ? transactionData.accountId
            : transactionData.toAccountId,
        type: transactionData.transactionType,
        amount: transactionData.amount,
        description: transactionData.description,
      };

      console.log('Transaction Data:', transactionData);
      console.log({ apiData });
      this.transactionsService.createTransaction(apiData).subscribe({
        next: () => {
          CommanAlert('Transaction success', AlertType.Success);
          this.router.navigate(['/transactions']);
        },
        error: (error) => {
          CommanAlert(error.error.message, AlertType.Error);
          console.error(error);
        },
      });
    }
  }
}
