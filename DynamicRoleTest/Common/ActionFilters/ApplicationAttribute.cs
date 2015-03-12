using System.Web.Mvc;

namespace DynamicRoleTest.Common.ActionFilters
{
    public class ApplicationAttribute : ActionFilterAttribute
    {
        public string Name { get; set; }
    }
}