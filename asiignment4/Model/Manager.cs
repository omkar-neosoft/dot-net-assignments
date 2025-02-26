namespace asiignment4.Model {
    public class Manager : Employee {
        public double Bonus { get; set; }

        public Manager(string name, double salary, double bonus)
            : base(name, salary) {
            Bonus = bonus;
        }

        public override void DisplayDetails() {
            Console.WriteLine($"Manager Name: {Name}, Salary: {Salary}, Bonus: {Bonus}");
        }
    }

}
