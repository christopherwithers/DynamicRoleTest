using System.Web.Mvc;
using DynamicRoleTest.Common.ActionFilters;

namespace DynamicRoleTest.Controllers
{
    [Application(Name = "Users")]
    public class UserController : Controller
    {
        
        [DynamicAuth(Name = "Index")]
        public ActionResult Index()
        {
            return View();
        }
        
        [DynamicAuth(Name = "List")]
        public ActionResult List()
        {
            return View();
        }
        
        [DynamicAuth(Name = "Create")]
        public ActionResult Create()
        {
            return View();
        }
    }
}