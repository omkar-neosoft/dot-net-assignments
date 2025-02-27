namespace Assignment_5.Repository {
    public abstract class VehicleInsurance {
        public string PolicyNumber { get; set; }
        public decimal InsuredAmount { get; set; }

        public VehicleInsurance(string policyNumber, decimal insuredAmount) {
            PolicyNumber = policyNumber;
            InsuredAmount = insuredAmount;
        }

        public abstract decimal CalculatePremium();
    }
}
