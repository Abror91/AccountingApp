using AccountingDemoApp.BLL.Infrastructure;
using AccountingDemoApp.BLL.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[assembly: OwinStartup(typeof(AccountingDemoApp.Web.App_Start.StartUp))]
namespace AccountingDemoApp.Web.App_Start
{
    public class StartUp
    {
        IServiceCreator serviceCreator = new ServiceCreator();
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<IUserService>(CreateUserService);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
        }
        private IUserService CreateUserService()
        {
            return serviceCreator.CreateUserService();
        }
    }
}