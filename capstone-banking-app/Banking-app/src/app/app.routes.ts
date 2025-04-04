import { Router, Routes } from '@angular/router';
import { HomeComponent } from './components/dashboard/home/home.component';
import { LoginComponent } from './components/auth/login/login.component';
import { RegisterComponent } from './components/auth/register/register.component';
import { inject } from '@angular/core';
import { AuthService } from './services/auth.service';
import { authGuard } from './guards/auth.guard';
import { AccountDashboardComponent } from './components/account/account-dashboard/account-dashboard.component';
import { CreateTransactionComponent } from './components/transactions/create-transaction/create-transaction.component';
import { TransactionDashboardComponent } from './components/transactions/transaction-dashboard/transaction-dashboard.component';
import { CreateAccountComponent } from './components/account/create-account/create-account.component';

export const routes: Routes = [
  { path: '', redirectTo: 'auth', pathMatch: 'full' },
  {
    path: 'auth',
    component: LoginComponent,
    canActivate: [
      () => {
        const authService = inject(AuthService);
        const router = inject(Router);
        if (authService.isLoggedIn()) {
          router.navigate(['/accounts']);
          return false;
        }
        return true;
      },
    ],
  },
  {
    path: 'register',
    component: RegisterComponent,
    canActivate: [
      () => {
        const authService = inject(AuthService);
        const router = inject(Router);
        if (authService.isLoggedIn()) {
          router.navigate(['/accounts']);
          return false;
        }
        return true;
      },
    ],
  },
  {
    path: 'accounts',
    component: AccountDashboardComponent,
    canActivate: [authGuard],
  },
  {
    path: 'accounts/create',
    component: CreateAccountComponent,
    canActivate: [authGuard],
  },
  {
    path: 'transactions',
    component: TransactionDashboardComponent,
    canActivate: [authGuard],
  },
  {
    path: 'transactions/create',
    component: CreateTransactionComponent,
    canActivate: [authGuard],
  },
  // {
  //   path: 'dashboard',
  //   component: HomeComponent,
  //   canActivate: [authGuard], // Protect dashboard route
  // },
  { path: '**', redirectTo: 'auth' }, // Wildcard route to handle unknown routes
];
