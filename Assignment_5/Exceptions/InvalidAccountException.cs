namespace Assignment_5.Exceptions {
    public class InvalidAccountException : ApplicationException {
        public InvalidAccountException(string message) : base(message) { }
    }
}
