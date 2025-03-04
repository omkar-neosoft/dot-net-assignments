using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using assignment_7.Models;

namespace assignment_7.Extensions
{
    public static class EmployeeExtensions
    {
        public static int GetYearsOfExperience(this Employee emp)
        {
            int years = DateTime.Now.Year - emp.JoiningDate.Year;
            if (emp.JoiningDate.Date > DateTime.Now.AddYears(-years))
            {
                years--;
            }
            return years;
        }
    }
}
