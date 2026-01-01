using Domain.Enums;
using Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Tickets
{
    public class Ticket: BaseEntity, IAggregateRoot
    {
        protected Ticket() { }
        public Ticket(string title, string description, Status status, Priority priority, Guid createdByUserId, Guid assignedToUserId)
        {
            Title = title;
            Description = description;
            Status = status;
            Priority = priority;
            CreatedByUserId = createdByUserId;
            AssignedToUserId = assignedToUserId;
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public Priority Priority { get; set; }
        public Guid CreatedByUserId { get; set; }
        public User CreatedBy { get; set; }
        public Guid AssignedToUserId { get; set; }
        public User AssignedTo { get; set; }


        public void Update(string title, string description, Status status, Priority priority, Guid createdByUserId, Guid assignedToUserId)
        {
            Title = title;
            Description = description;
            Status = status;
            Priority = priority;
            CreatedByUserId = createdByUserId;
            AssignedToUserId = assignedToUserId;

            UpdatedAt = DateTime.UtcNow;
        }
    }
}
