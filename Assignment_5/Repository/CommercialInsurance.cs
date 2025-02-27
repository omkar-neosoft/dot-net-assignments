namespace Assignment_5.Repository {
    public class CommercialInsurance : VehicleInsurance {
        public CommercialInsurance(string policyNumber, decimal insuredAmount)
            : base(policyNumber, insuredAmount) { }

        public override decimal CalculatePremium() {
            return InsuredAmount * 0.07m;
        }
    }
}
