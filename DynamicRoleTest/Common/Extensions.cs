using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using DynamicRoleTest.Common.ActionFilters;

namespace DynamicRoleTest.Common
{
    public static class Extensions
    {
        public static IEnumerable<Type> GetDynamicAuthApplications(this Assembly assembly)
        {
            foreach (var type in assembly.GetTypes())
            {
                if (type.GetCustomAttributes(typeof(DynamicAuthAttribute), true).Length > 0)
                {
                    yield return type;
                }
            }

        }

        public static IEnumerable<string> GetDynamicAuthApplicationsAtt(this Assembly assembly)
        {
            foreach (var type in assembly.GetTypes())
            {
                foreach (var attribute in type.GetMethods())
                {
                    var fff = attribute.GetCustomAttributes(typeof(DynamicAuthAttribute), true).FirstOrDefault();

                    if (fff != null)
                    {
                        yield return string.Format("{0} -> {1}", type.Name, fff.GetType().GetProperty("Name").GetValue(fff));
                    }
                }
            }

        }

        public static IEnumerable<string> GetDynamicAuthApplicationsAtt1(this Assembly assembly)
        {
            foreach (var type in assembly.GetTypes())
            {
                var typeA = type.GetCustomAttributes(typeof (ApplicationAttribute), true).FirstOrDefault();
                if (typeA == null) continue;

                var appname = typeA.GetType().GetProperty("Name").GetValue(typeA).ToString();
                   
                foreach (var attribute in type.GetMethods())
                {
                    var fff = attribute.GetCustomAttributes(typeof(DynamicAuthAttribute), true).FirstOrDefault();

                    if (fff != null)
                    {
                        yield return string.Format("{0} -> {1}", appname, fff.GetType().GetProperty("Name").GetValue(fff));
                    }
                }
            }


        }
    }
}