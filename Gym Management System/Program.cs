using BLL.MappingProfiles;
using BLL.MappingProfiles.MemberMappingProfiles;
using BLL.MappingProfiles.SessionMappingProfiles;
using BLL.Services.AccountServices;
using BLL.Services.AnalyticsServices;
using BLL.Services.AttachmentService;
using BLL.Services.Member;
using BLL.Services.PlanServices;
using BLL.Services.SessionServices;
using BLL.Services.TrainerServices;
using DAL.Data.Contexts;
using DAL.Data.DataSeeding;
using DAL.Entities.ApplicationUsers;
using DAL.Repositories.GenaricRepo;
using DAL.Repositories.SessionRepo;
using DAL.UOfW;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Gym_Management_System
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<GymDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
            builder.Services.AddScoped<ISessionRepository,SessionRepository>();
            builder.Services.AddScoped<IAnalyticsService,AnalyticsService>();
            builder.Services.AddScoped<IMemberService, MemberService>();
            builder.Services.AddScoped<ITrainerService, TrainerService>();
            builder.Services.AddScoped<IPlanService, PlanService>();
            builder.Services.AddScoped<ISessionService, SessionService>();
            builder.Services.AddScoped<IAttachmentService, AttachmentService>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddAutoMapper(x => x.AddProfile(new SessionMappingProfile()));
            builder.Services.AddAutoMapper(x=>x.AddProfile(new MemberMappingProfile()));
            builder.Services.AddAutoMapper(x=>x.AddProfile(new PlanMappingProfile()));
            builder.Services.AddAutoMapper(x=>x.AddProfile(new TrainerMappingProfile()));
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(
                config =>
                {
                    config.User.RequireUniqueEmail = true;
                }
                ).AddEntityFrameworkStores<GymDbContext>();
            builder.Services.ConfigureApplicationCookie(op =>
            {
                op.AccessDeniedPath = "/Account/AccessDenied";
                op.LoginPath = "/Account/Login";

            });
            var app = builder.Build();

            #region DataSeeding
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<GymDbContext>();
            var RoleManger = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManger = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var PendingMigrations = context.Database.GetPendingMigrations();
            if (PendingMigrations?.Any() ?? false) context.Database.Migrate();
            GymDBContextSeeding.SeedData(context);
            IdentityDbContextSeeding.SeedAsync(RoleManger, UserManger);
            #endregion

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();   

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}");
                

            app.Run();
        }
    }
}
