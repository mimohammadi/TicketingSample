using Domain.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Configuration
{
    internal class UserEfconfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(a => a.Email).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Password).IsRequired().HasMaxLength(1000);
            builder.Property(x => x.Role).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();


            builder.HasMany(x=>x.AssignedTickets).WithOne(a=>a.AssignedTo).HasForeignKey(b=>b.AssignedToUserId);
            builder.HasMany(x=>x.CreatedTickets).WithOne(a=>a.CreatedBy).HasForeignKey(b=>b.CreatedByUserId);


            builder.HasQueryFilter(a => a.IsDeleted == false);
        }
    }
}
