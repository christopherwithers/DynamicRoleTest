using System.Collections.Generic;

namespace DynamicRoleTest.Common
{
    public class ApplicationAccess
    {
        public ApplicationAccess()
        {
            Pages = new List<PageAccess>();
        }

        public string Name { get; set; }

        public List<PageAccess> Pages { get; set; } 
    }

    public class PageAccess
    {
        public string Name { get; set; }
    }
}