using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akka.Demo
{
    public class EmailNotification : IEmailNotification
    {
        public void Send(string message) 
        {
            Console.WriteLine($"Sending email with message: {message}");
        }
    }
}
