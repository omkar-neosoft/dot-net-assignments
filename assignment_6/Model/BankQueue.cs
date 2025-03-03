using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment_6.Model
{
    internal class BankQueue
    {
        private Queue<int> tokenQueue = new Queue<int>();
        private int nextToken = 1;

        public void AddCustomer()
        {
            tokenQueue.Enqueue(nextToken);
            Console.WriteLine($"Token {nextToken} added to the queue.");
            nextToken++;
        }

        public void ServeCustomer()
        {
            if (tokenQueue.Count > 0)
            {
                int servedToken = tokenQueue.Dequeue();
                Console.WriteLine($"Serving customer with token {servedToken}.");
            }
            else
            {
                Console.WriteLine("No customers in the queue.");
            }
        }

        public void CheckNextCustomer()
        {
            if (tokenQueue.Count > 0)
            {
                Console.WriteLine($"Next customer to be served has token {tokenQueue.Peek()}.");
            }
            else
            {
                Console.WriteLine("No customers in the queue.");
            }
        }
    }
}
