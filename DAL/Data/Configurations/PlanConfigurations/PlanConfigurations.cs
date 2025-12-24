using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities.Plan;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Data.Configurations.PlanConfigurations
{
    internal class PlanConfigurations : IEntityTypeConfiguration<Plan>
    {
        public void Configure(EntityTypeBuilder<Plan> builder)
        {
            builder.Property(X => X.Name)
                .HasColumnType("Varchar")
                .HasMaxLength(50);
            builder.Property(X => X.Description)
                .HasColumnType("Varchar")
                .HasMaxLength(200);
            builder.Property(X => X.Price)
                .HasColumnType("Decimal")
                .HasPrecision(10, 2);
            builder.ToTable(TB =>
            {
                TB.HasCheckConstraint("PlanDurationCheck", "DurationDays Between 1 AND 365");
            });

        }
    }
}
