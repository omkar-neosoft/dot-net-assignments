namespace assignment3.Model {
    public class Car {
        public int CarID { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public double Price { get; set; }

        // Constructor
        public Car() {

        }

        // Constructor
        public Car(int carID, string brand, string model, int year, double price) {
            CarID = carID;
            Brand = brand;
            Model = model;
            Year = year;
            Price = price;
        }

        public void AcceptCarDetails() {
            Console.WriteLine("Receiving Car Information");

            Console.Write("Enter Car ID: ");
            CarID = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Car Brand: ");
            Brand = Console.ReadLine();

            Console.Write("Enter Car Model: ");
            Model = Console.ReadLine();

            Console.Write("Enter Manufacturing Year: ");
            Year = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Car Price (Rs): ");
            Price = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Car information received successfully!");
        }

        // Member function to display car details
        public void DisplayCarDetails() {
            Console.WriteLine("\nPresenting Car Information");

            Console.WriteLine($"Car ID: {CarID}");
            Console.WriteLine($"Brand: {Brand}");
            Console.WriteLine($"Model: {Model}");
            Console.WriteLine($"Year: {Year}");
            Console.WriteLine($"Price: Rs {Price}");
        }
    }
}

