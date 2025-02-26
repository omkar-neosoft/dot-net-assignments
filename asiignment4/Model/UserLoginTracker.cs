namespace asiignment4.Model {
    internal class UserLoginTracker {
        private static int totalUsersLoggedIn = 0;

        public UserLoginTracker(string userName) {
            totalUsersLoggedIn++;
            Console.WriteLine($"'{userName}' has logged in. Total users logged in: {totalUsersLoggedIn}");
        }

        public static int GetTotalUsersLoggedIn() {
            return totalUsersLoggedIn;
        }
    }
}
