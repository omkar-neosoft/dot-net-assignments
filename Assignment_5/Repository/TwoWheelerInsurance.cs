namespace Assignment_5.Repository {

    public class TwoWheelerInsurance : VehicleInsurance {
        public TwoWheelerInsurance(string policyNumber, decimal insuredAmount)
            : base(policyNumber, insuredAmount) { }

        public override decimal CalculatePremium() {
            return InsuredAmount * 0.02m;
        }
    }
}
