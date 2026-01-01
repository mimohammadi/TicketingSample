using Domain.Enums;
using Domain.Models.Tickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Users
{
    public class User: BaseEntity,IAggregateRoot
    {
        protected User() { }

        public User(string fullName, string email, string password, Role role)
        {
            Role = role;
            FullName = fullName;
            Email = email;
            Password = password;
        }

        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }

        public ICollection<Ticket> CreatedTickets { get; set; } 
        public ICollection<Ticket> AssignedTickets { get; set; }
    }
}
