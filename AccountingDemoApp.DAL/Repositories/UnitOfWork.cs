using AccountingDemoApp.DAL.Entities;
using AccountingDemoApp.DAL.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingDemoApp.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationRoleManager _roleManager;
        private readonly IExpenditureRepository _expenditures;
        private readonly ICategoryRepository _categories;
        public UnitOfWork()
        {
            _db = new ApplicationDbContext();
            _userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(_db));
            _roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(_db));
            _expenditures = new ExpenditureRepository(_db);
            _categories = new CategoryRepository(_db);
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                _userManager.UserValidator = new UserValidator<ApplicationUser>(_userManager)
                {
                    AllowOnlyAlphanumericUserNames = false,
                    RequireUniqueEmail = true
                };
                _userManager.PasswordValidator = new PasswordValidator
                {
                    RequiredLength = 6,
                    RequireNonLetterOrDigit = false,
                    RequireDigit = true,
                    RequireLowercase = true,
                    RequireUppercase = false,
                };
                return _userManager;
            }
        }
        public ApplicationRoleManager RoleManager { get { return _roleManager; } }
        public IExpenditureRepository Expenditures { get { return _expenditures; } }
        public ICategoryRepository Categories { get { return _categories; } }
        public async Task SaveChanges()
        {
            await _db.SaveChangesAsync();
        }

        public void Dispose()
        {
            _userManager.Dispose();
            _roleManager.Dispose();
            _expenditures.Dispose();
            _categories.Dispose();
        }
    }
}
