using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }

        public ICollection<Order> Orders { get; set; } = null!;

        public override string? ToString()
        {
            string ret = $"""
                First Name:     {FirstName}
                Last Name:      {LastName}
                Address:        {Address}
                Phone:          {Phone}
                Email:          {Email}
                """;
            return ret;
        }
    }
}
