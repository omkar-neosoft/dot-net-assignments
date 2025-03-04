using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment_7.Models
{
    public class Employee
    {
        public string Name { get; set; }
        public DateTime JoiningDate { get; set; }

        public Employee(string name, DateTime joiningDate)
        {
            Name = name;
            JoiningDate = joiningDate;
        }
    }
}
