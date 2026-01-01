using Domain.Models.Tickets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Configuration
{
    internal class TicketEfConfig : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.Property(a => a.Title).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.Priority).IsRequired();
            builder.Property(x => x.CreatedByUserId).IsRequired();
            builder.Property(x => x.AssignedToUserId).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();

            builder.HasQueryFilter(a => a.IsDeleted == false);
        }
    }
}
