using Microsoft.EntityFrameworkCore;
using ProblemTracking.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ProblemTracking.Entity;
using Microsoft.AspNetCore.Http;
using System.Reflection.Emit;
//using System.Reflection.PortableExecutable;

namespace ProblemTracking.Entity
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Problem> Problem { get; set; }
        public DbSet<InvestigateStep> InvestigateStep { get; set; }
        public DbSet<Machine> Machine { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<ProblemInvestigate> ProblemInvestigate { get; set; }


        public UserData CurrentUserData { get; private set; } = UserData.SystemUserData;
        protected readonly IHttpContextAccessor HttpContextAccessor;
        protected readonly IServiceProvider ServiceProvider;

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public ApplicationDbContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor, IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            HttpContextAccessor = httpContextAccessor;
            CurrentUserData = HttpContextAccessor.HttpContext?.User?.AsUserData() ?? UserData.SystemUserData;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("dbo");

            //Seeder(modelBuilder);

        }
        private void Seeder(ModelBuilder modelBuilder) {

            modelBuilder.Entity<Machine>().HasData(
                 new
                 {
                     Id = 1,
                     MachineName = "Machine1",
                     MachineDescription = "This machine use outside factory plaese investigate dust issue.",
                     CreatedDate = DateTime.Now,
                     //CreatedUserId = null,
                 },
                 new
                 {
                     Id = 2,
                     MachineName = "Machine1",
                     MachineDescription = "This machine use outside factory plaese investigate dust issue.",
                     CreatedDate = DateTime.Now,

                 },
                 new
                 {
                     Id = 3,
                     MachineName = "Machine1",
                     MachineDescription = "This machine use outside factory plaese investigate dust issue.",
                     CreatedDate = DateTime.Now,
                     //CreatedUserId = null,
                 }
               );
            modelBuilder.Entity<InvestigateStep>().HasData(new
            {
                Id = 1,
                StepName = "Step1 Check surface",
                StepDetail = "Normally, this machince stuck because of some object break it.",
                CreatedDate = DateTime.Now,
                MachineId = 1,
                Order = 1,
                //CreatedUserId = null
            },
            new
            {
                Id = 2,
                StepName = "Step2 Check electricity",
                StepDetail = "Make sure you not unplug it.",
                CreatedDate = DateTime.Now,
                MachineId = 1,
                Order = 2,
                //CreatedUserId = null
            },
            new
            {
                Id = 3,
                StepName = "Step3 Check all liquid",
                StepDetail = "Sometime we use it all day.",
                CreatedDate = DateTime.Now,
                MachineId = 1,
                Order = 3,
                //CreatedUserId = null
            },
             new
             {
                 Id = 4,
                 StepName = "Step1 Check surface 2 ",
                 StepDetail = "Normally, this machince stuck because of some object break it.2",
                 CreatedDate = DateTime.Now,
                 MachineId = 2,
                 Order = 1,
                 //CreatedUserId = null
             },
            new
            {
                Id = 5,
                StepName = "Step2 Check electricity 2",
                StepDetail = "Make sure you not unplug it.2",
                CreatedDate = DateTime.Now,
                MachineId = 2,
                Order = 2,
                // CreatedUserId = null
            },
            new
            {
                Id = 6,
                StepName = "Step3 Check all liquid 2",
                StepDetail = "Sometime we use it all day.2",
                CreatedDate = DateTime.Now,
                MachineId = 2,
                Order = 3,
                //CreatedUserId = null
            },
                 new
                 {
                     Id = 7,
                     StepName = "Step1 Check surface 3 ",
                     StepDetail = "Normally, this machince stuck because of some object break it.3",
                     MachineId = 3,
                     CreatedDate = DateTime.Now,
                     Order = 1,
                     //CreatedUserId = null,

                 },
            new
            {
                Id = 8,
                StepName = "Step2 Check electricity 3",
                StepDetail = "Make sure you not unplug it.3",
                MachineId = 3,
                CreatedDate = DateTime.Now,
                Order = 2,
                //CreatedUserId = null
            },
            new
            {
                Id = 9,
                StepName = "Step3 Check all liquid 2",
                StepDetail = "Sometime we use it all day.3",
                MachineId = 3,
                CreatedDate = DateTime.Now,
                Order = 3,
                //CreatedUserId = null
            });
            modelBuilder.Entity<User>().HasData(
              new User
              {
                  Id = Guid.NewGuid(),
                  UserName = "User1",
                  Password = "12345",
                  FirstName = "Hamlet",
                  LastName = "last 1",
                  CreatedDate = DateTime.Now
              },
              new User
              {
                  Id = Guid.NewGuid(),
                  UserName = "User2",
                  Password = "12345",
                  FirstName = "King Lear",
                  LastName = "last 2",
                  CreatedDate = DateTime.Now
              },
              new User
              {
                  Id = Guid.NewGuid(),
                  UserName = "User3",
                  Password = "12345",
                  FirstName = "Othello",
                  LastName = "last 3",
                  CreatedDate = DateTime.Now
              }
            );

        }
    }
}
