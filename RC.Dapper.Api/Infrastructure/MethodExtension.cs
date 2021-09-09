using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace RC.Dapper.Api.Infrastructure
{
    public static class MethodExtension
    {
        public static IEnumerable<string> GenerateListOfProperties(IEnumerable<PropertyInfo> listOfProperties)
        {
            return (from prop in listOfProperties
                    let attributes = prop.GetCustomAttributes(typeof(DescriptionAttribute), false)
                    where attributes.Length <= 0 || (attributes[0] as DescriptionAttribute)?.Description != "ignore"
                    select prop.Name).ToList();
        }
    }
}