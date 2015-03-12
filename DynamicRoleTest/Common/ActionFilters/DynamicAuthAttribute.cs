using System.Web.Mvc;

namespace DynamicRoleTest.Common.ActionFilters
{
    public class DynamicAuthAttribute : ActionFilterAttribute
    {
        public string Name { get; set; }
    }
}