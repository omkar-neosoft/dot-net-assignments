namespace asiignment4.Model {
    public class Employee {
        public string Name { get; set; }
        public double Salary { get; set; }

        public Employee(string name, double salary) {
            Name = name;
            Salary = salary;
        }

        public virtual void DisplayDetails() {
            Console.WriteLine($"Employee Name: {Name}, Salary: {Salary:C}");
        }
    }
}
