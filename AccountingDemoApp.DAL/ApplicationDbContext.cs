using AccountingDemoApp.DAL.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingDemoApp.DAL
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public ApplicationDbContext() : base("DefaultConnection")
        {

        }
        public DbSet<Expenditure> Expenditures { get; set; }
        public DbSet<Category> Categories { get; set; }
        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
        public override Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }
        public new DbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }
        public new DbEntityEntry Entry<T>(T t) where T : class
        {
            return base.Entry(t);
        }
    }
}
