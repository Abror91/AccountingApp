using AccountingDemoApp.BLL.Interfaces;
using AccountingDemoApp.BLL.Services;
using AccountingDemoApp.DAL;
using AccountingDemoApp.DAL.Interfaces;
using AccountingDemoApp.DAL.Repositories;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingDemoApp.BLL.Ninject
{
    public class NinjectInitializer : NinjectModule
    {
        public override void Load()
        {
            Bind<IApplicationDbContext>().To<ApplicationDbContext>();
            Bind<IUnitOfWork>().To<UnitOfWork>();
            Bind<IExpenditureRepository>().To<ExpenditureRepository>();
            Bind<ICategoryRepository>().To<CategoryRepository>();
            Bind<IExpenditureService>().To<ExpenditureService>();
            Bind<ICategoryService>().To<CategoryService>();
            Bind<IUserService>().To<UserService>();
            Bind<IRoleService>().To<RoleService>();
        }
    }
}
