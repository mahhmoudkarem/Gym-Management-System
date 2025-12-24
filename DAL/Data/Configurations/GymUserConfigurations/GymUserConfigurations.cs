using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Data.Configurations.GymUserConfigurations
{
    internal class GymUserConfigurations<T> : IEntityTypeConfiguration<T> where T : GymUser
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(x => x.Name)
                .HasColumnType("varchar")
                .HasMaxLength(50);
            builder.Property(x => x.Email)
                .HasColumnType("varchar")
                .HasMaxLength(100);
            builder.Property(x => x.Phone)
                .HasColumnType("varchar")
                .HasMaxLength(11);
            builder.ToTable(TB => TB.HasCheckConstraint("GymUserValidEmailCheck", "Email Like '_%@_%._%'"));
            builder.ToTable(TB => TB.HasCheckConstraint("GymUserValidEmailCheck", "Phone Like '01%' AND Phone Not Like '%[^0-9]%'"));

            builder.HasIndex(I => I.Email).IsUnique();
            builder.HasIndex(I => I.Phone).IsUnique();

            builder.OwnsOne(X => X.Address, AddressBuilder =>
            {
                AddressBuilder.Property(X => X.Street)
                .HasMaxLength(30)
                .HasColumnType("Varchar")
                .HasColumnName("Street");
                AddressBuilder.Property(X => X.City)
                .HasMaxLength(15)
                .HasColumnType("Varchar")
                .HasColumnName("City");
                AddressBuilder.Property(X => X.BuildingNumber)
                .HasColumnName("BuildingNumber");
            });
                

        }
    }
}
