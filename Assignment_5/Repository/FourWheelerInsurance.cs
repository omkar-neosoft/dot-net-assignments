namespace Assignment_5.Repository {
    public class FourWheelerInsurance : VehicleInsurance {
        public FourWheelerInsurance(string policyNumber, decimal insuredAmount)
            : base(policyNumber, insuredAmount) { }

        public override decimal CalculatePremium() {
            return InsuredAmount * 0.05m;
        }
    }
}
