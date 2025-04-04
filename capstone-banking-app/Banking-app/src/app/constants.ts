import Swal from 'sweetalert2';

// const apiBaseUrl = 'http://localhost:5271';
const apiBaseUrl = 'https://localhost:7134';

const apiUrls = {
  loginUrl: `${apiBaseUrl}/api/Auth/login`,
  regiterUrl: `${apiBaseUrl}/api/Auth/register`,
  getLoggedInUserAccountId: `${apiBaseUrl}/api/Accounts/user`,
  createTransaction: `${apiBaseUrl}/api/Transactions`,
  // getUserTransactions: `${apiBaseUrl}/api/Transactions/account`,
  getUserTransactions: `${apiBaseUrl}/api/Transactions/user`,
  createAccount: `${apiBaseUrl}/api/Accounts`,
};

export const APP_CONSTANTS = {
  apiBaseUrl,
  apiUrls,
  defaultTimeout: 5000,
  appName: 'Banking Application',
};

export const TransactionType = ['Deposite', 'Withdrawal', 'Transfer'];
export const AccountType = ['Savings', 'Current'];

export const AlertType = {
  Success: 'success',
  Error: 'error',
};
export function CommanAlert(
  message: string = 'Internal server error',
  alertType: string = 'error'
) {
  Swal.fire({
    html: `<p style="font-size: 18px;">${message}</p>`,
    icon: alertType === 'success' ? 'success' : 'error',
    confirmButtonText: 'Ok',
    customClass: {
      popup: 'small-swal',
    },
  });
}
