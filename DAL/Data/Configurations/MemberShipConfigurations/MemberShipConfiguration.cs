using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities.RelationalEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Data.Configurations.MemberShipConfigurations
{
    internal class MemberShipConfiguration : IEntityTypeConfiguration<MemberShip>
    {

        public void Configure(EntityTypeBuilder<MemberShip> builder)
        {
            builder.Property(X => X.CreatedAt)
                .HasColumnName("StartDate")
                .HasDefaultValueSql("GETDATE()");
            builder.HasKey(X => new { X.MemberId, X.PlanId });
            builder.Ignore(X=>X.Id);

        }
    }
}
