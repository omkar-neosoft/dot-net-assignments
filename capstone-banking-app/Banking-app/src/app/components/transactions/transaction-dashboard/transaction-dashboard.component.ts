import { Component, inject, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { TransactionsService } from '../../../services/transactions.service';
import { MatButtonModule } from '@angular/material/button';
import { DatePipe } from '@angular/common';
import { AuthService } from '../../../services/auth.service';
import { TransactionType } from '../../../constants';
import { RouterModule } from '@angular/router';
import { MatSort, MatSortModule, Sort } from '@angular/material/sort';

interface Transaction {
  id: string;
  fromAccountId: string;
  type: number;
  amount: number;
  description: string;
  date: string;
}

@Component({
  selector: 'app-transaction-dashboard',
  standalone: true,
  imports: [
    MatTableModule,
    MatButtonModule,
    MatSortModule,
    DatePipe,
    RouterModule,
  ],
  templateUrl: './transaction-dashboard.component.html',
  styleUrl: './transaction-dashboard.component.css',
})
export class TransactionDashboardComponent implements OnInit {
  displayedColumns: string[] = [
    'id',
    'type',
    'amount',
    'description',
    'fromAccountId',
    'date',
  ];
  dataSource = new MatTableDataSource<Transaction>();
  userId: string = '';
  accountId: string = '';
  transactionType = TransactionType;

  @ViewChild(MatSort) sort!: MatSort;

  private transactionsService = inject(TransactionsService);
  private authService = inject(AuthService);

  ngOnInit() {
    this.userId = this.authService.getUserId() || '';
    this.loadTransactions();
    // this.transactionsService.getLoggedInUserAccountId(this.userId).subscribe({
    //   next: (response) => {
    //     // console.log({ response });
    //     this.accountId = response[0].id;
    //     this.loadTransactions();
    //   },
    // });
  }

  loadTransactions() {
    this.transactionsService
      .getUserTransactions(this.userId)
      .subscribe((data) => {
        this.dataSource.data = data;
        this.dataSource.sort = this.sort;

        const sortState: Sort = { active: 'date', direction: 'desc' };
        this.sort.active = sortState.active;
        this.sort.direction = sortState.direction;
        this.sort.sortChange.emit(sortState);
      });
  }

  openCreateTransaction() {
    // Navigate to the create transaction form (you can use router.navigate)
    console.log('Open Create Transaction Modal');
  }
}
