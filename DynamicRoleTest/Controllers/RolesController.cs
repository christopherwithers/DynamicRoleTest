using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.Mvc;
using DynamicRoleTest.Common.ActionFilters;
using DynamicRoleTest.Common;

namespace DynamicRoleTest.Controllers
{
    [Application(Name="Roles")]
    public class RolesController : Controller
    {
        public IEnumerable<ApplicationAccess> GetDynamicAuthApplicationsClass()
        {
            var assembly = typeof(RolesController).Assembly;
            var applications = new Collection<ApplicationAccess>();
            foreach (var type in assembly.GetTypes())
            {
                var typeA = type.GetCustomAttributes(typeof(ApplicationAttribute), true).FirstOrDefault();
                if (typeA == null) continue;

                var appname = typeA.GetType().GetProperty("Name").GetValue(typeA).ToString();

                var application = new ApplicationAccess {Name = appname};

                foreach (var attribute in type.GetMethods())
                {
                    var fff = attribute.GetCustomAttributes(typeof(DynamicAuthAttribute), true).FirstOrDefault();

                    if (fff != null)
                    {
                        application.Pages.Add(new PageAccess() { Name = fff.GetType().GetProperty("Name").GetValue(fff).ToString() });
                    }
                }

                applications.Add(application);
            }

            return applications;
        }

        [DynamicAuth(Name = "tester1")]
        public ActionResult Index()
        {
            var applications = GetDynamicAuthApplicationsClass();

            return View(applications);
        }

        [DynamicAuth(Name = "sdfsdfds")]
        public ActionResult Index3()
        {
            var types = typeof(RolesController).Assembly.GetDynamicAuthApplicationsAtt();

            return View(types);
        }
    }
}