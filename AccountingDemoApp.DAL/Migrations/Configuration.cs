namespace AccountingDemoApp.DAL.Migrations
{
    using AccountingDemoApp.DAL.Entities;
    using AccountingDemoApp.DAL.Repositories;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Threading.Tasks;

    internal sealed class Configuration : DbMigrationsConfiguration<AccountingDemoApp.DAL.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AccountingDemoApp.DAL.ApplicationDbContext db)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            var roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(db));
            if (!roleManager.Roles.Any())
            {
                roleManager.Create(new ApplicationRole { Name = "Admin" });
                roleManager.Create(new ApplicationRole { Name = "User" });

            }
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            if (!userManager.Users.Any())
            {
                var admin = new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = "jason@jason.com",
                    UserName = "jason@jason.com",
                    EmailConfirmed = true,
                    Deposit = 25000m
                };
                userManager.Create(admin, "jason12345");
                userManager.AddToRoles(admin.Id, new string[] { "Admin", "User" });
            }
            SaveData(db);
        }
        private void SaveData(ApplicationDbContext db)
        {
           
            var categories = new List<Category>()
            {
                new Category { Name = "Интернет" },
                new Category { Name = "Мобильная связь" },
                new Category { Name = "Продукты питания" },
                new Category { Name = "Развлечения" },
                new Category { Name = "Транспорт" }
            };
            categories.ForEach(s => db.Categories.AddOrUpdate(p => p.Name, s));
            db.SaveChanges();
            var expenditures = new List<Expenditure>()
            {
                new Expenditure
                {
                    ExpenseTime = new DateTime(2019, 1, 10),
                    CategoryId = db.Categories.Where(s => s.Name == "Интернет").SingleOrDefault().Id,
                    Cost = 25.15m,
                    Comment = "Uzonline",
                    ApplicationUserId = db.Users.Where(s => s.UserName == "jason@jason.com").SingleOrDefault().Id
                },
                new Expenditure
                {
                    ExpenseTime = new DateTime(2019, 2, 10),
                    CategoryId = db.Categories.Where(s => s.Name == "Интернет").SingleOrDefault().Id,
                    Cost = 25.15m,
                    Comment = "Sarkor",
                    ApplicationUserId = db.Users.Where(s => s.UserName == "jason@jason.com").SingleOrDefault().Id
                },
                new Expenditure
                {
                    ExpenseTime = new DateTime(2019, 3, 10),
                    CategoryId = db.Categories.Where(s => s.Name == "Мобильная связь").SingleOrDefault().Id,
                    Cost = 20.25m,
                    Comment = "Абонентская плата",
                    ApplicationUserId = db.Users.Where(s => s.UserName == "jason@jason.com").SingleOrDefault().Id
                },
                new Expenditure
                {
                    ExpenseTime = new DateTime(2019, 4, 10),
                    CategoryId = db.Categories.Where(s => s.Name == "Мобильная связь").SingleOrDefault().Id,
                    Cost = 55.10m,
                    Comment = "Мобильный интернет",
                    ApplicationUserId = db.Users.Where(s => s.UserName == "jason@jason.com").SingleOrDefault().Id
                },
                new Expenditure
                {
                    ExpenseTime = new DateTime(2019, 5, 10),
                    CategoryId = db.Categories.Where(s => s.Name == "Продукты питания").SingleOrDefault().Id,
                    Cost = 100.15m,
                    Comment = "Мясо",
                    ApplicationUserId = db.Users.Where(s => s.UserName == "jason@jason.com").SingleOrDefault().Id
                },
                new Expenditure
                {
                    ExpenseTime = new DateTime(2019, 6, 10),
                    CategoryId = db.Categories.Where(s => s.Name == "Продукты питания").SingleOrDefault().Id,
                    Cost = 250.25m,
                    Comment = "Фрукты",
                    ApplicationUserId = db.Users.Where(s => s.UserName == "jason@jason.com").SingleOrDefault().Id
                },
                 new Expenditure
                {
                    ExpenseTime = new DateTime(2019, 7, 10),
                    CategoryId = db.Categories.Where(s => s.Name == "Развлечения").SingleOrDefault().Id,
                    Cost = 25.25m,
                    Comment = "Концерт",
                    ApplicationUserId = db.Users.Where(s => s.UserName == "jason@jason.com").SingleOrDefault().Id
                },
                new Expenditure
                {
                    ExpenseTime = new DateTime(2019, 8, 10),
                    CategoryId = db.Categories.Where(s => s.Name == "Развлечения").SingleOrDefault().Id,
                    Cost = 150.10m,
                    Comment = "Ресторан",
                    ApplicationUserId = db.Users.Where(s => s.UserName == "jason@jason.com").SingleOrDefault().Id
                },
                new Expenditure
                {
                    ExpenseTime = new DateTime(2019, 9, 10),
                    CategoryId = db.Categories.Where(s => s.Name == "Транспорт").SingleOrDefault().Id,
                    Cost = 100m,
                    Comment = "Такси",
                    ApplicationUserId = db.Users.Where(s => s.UserName == "jason@jason.com").SingleOrDefault().Id
                },
                new Expenditure
                {
                    ExpenseTime = new DateTime(2019, 10, 10),
                    CategoryId = db.Categories.Where(s => s.Name == "Транспорт").SingleOrDefault().Id,
                    Cost = 200m,
                    Comment = "Проездной",
                    ApplicationUserId = db.Users.Where(s => s.UserName == "jason@jason.com").SingleOrDefault().Id
                },
            };
            expenditures.ForEach(s => db.Expenditures.AddOrUpdate(p => p.ExpenseTime, s));
            db.SaveChanges();
        }

        private void InditializeAdminAndRolesData(ApplicationDbContext db)
        {
            var roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(db));
            if (!roleManager.Roles.Any())
            {
                 roleManager.CreateAsync(new ApplicationRole { Name = "Admin" });
                 roleManager.CreateAsync(new ApplicationRole { Name = "User" });

            }
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            if (!userManager.Users.Any())
            {
                var admin = new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = "jason@jason.com",
                    UserName = "jason@jason.com",
                    EmailConfirmed = true
                };
                 userManager.CreateAsync(admin, "jason12345");
                 userManager.AddToRolesAsync(admin.Id, new string[] { "Admin", "User" });
            }
        }
    }
}
