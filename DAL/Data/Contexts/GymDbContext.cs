using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities.ApplicationUsers;
using DAL.Entities.Category;
using DAL.Entities.HealthRecord;
using DAL.Entities.MemberEntity;
using DAL.Entities.Plan;
using DAL.Entities.RelationalEntities;
using DAL.Entities.Session;
using DAL.Entities.Trainer;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data.Contexts
{
    public class GymDbContext :IdentityDbContext<ApplicationUser>
    {
        public GymDbContext(DbContextOptions<GymDbContext> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Entity<ApplicationUser>(e =>
            {
                e.Property(x => x.FirstName).HasColumnType("varchar").HasMaxLength(50);
                e.Property(x => x.LastName).HasColumnType("varchar").HasMaxLength(50);
            });
        }


        #region DBSets


        public DbSet<Member> Members { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<HealthRecord> HealthRecords { get; set; }
        public DbSet<MemberShip> MembersShips { get; set; }
        public DbSet<MemberSession> MemberSessions { get; set; }

        #endregion
    }
}
