using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment_6.Model
{
    internal class UniversityEvent
    {
        private Dictionary<string, HashSet<int>> workshopRegistrations = new Dictionary<string, HashSet<int>>();

        public void RegisterStudent(string workshop, int studentId)
        {
            if (!workshopRegistrations.ContainsKey(workshop))
            {
                workshopRegistrations[workshop] = new HashSet<int>();
            }

            if (workshopRegistrations[workshop].Add(studentId))
            {
                Console.WriteLine($"Student {studentId} registered for {workshop}.");
            }
            else
            {
                Console.WriteLine($"Student {studentId} is already registered for {workshop}.");
            }
        }

        public void DisplayRegistrations(string workshop)
        {
            if (workshopRegistrations.ContainsKey(workshop) && workshopRegistrations[workshop].Count > 0)
            {
                Console.WriteLine($"Registered students for {workshop}: {string.Join(", ", workshopRegistrations[workshop])}");
            }
            else
            {
                Console.WriteLine($"No students registered for {workshop}.");
            }
        }
    }
}
